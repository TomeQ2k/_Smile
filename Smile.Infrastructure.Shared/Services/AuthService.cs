using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Builders;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabase database;
        private readonly IRolesService rolesService;
        private readonly IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator;
        private readonly IHashGenerator hashGenerator;

        public AuthService(IDatabase database, IRolesService rolesService, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IHashGenerator hashGenerator)
        {
            this.database = database;
            this.rolesService = rolesService;
            this.jwtAuthorizationTokenGenerator = jwtAuthorizationTokenGenerator;
            this.hashGenerator = hashGenerator;
        }

        public async Task<AuthResult> SignIn(string email, string password)
        {
            var user = await database.UserRepository.Find(u => u.Email.ToLower() == email.ToLower()) ?? throw new InvalidCredentialsException("Invalid email or password");

            if (!user.EmailConfirmed)
                throw new AccountNotConfirmedException("Account has not been activated");

            if (user.IsBlocked)
                throw new BlockException("Your account is blocked");

            if (hashGenerator.VerifyHash(password, user.PasswordHash, user.PasswordSalt))
            {
                var token = await jwtAuthorizationTokenGenerator.GenerateToken(user);

                return new AuthResult(token, user);
            }

            throw new InvalidCredentialsException("Invalid email or password");
        }

        public async Task<AuthResult> SignUp(string email, string password, string username)
        {
            string saltedPasswordHash = string.Empty;
            var passwordSalt = hashGenerator.CreateSalt();

            hashGenerator.GenerateHash(password, passwordSalt, out saltedPasswordHash);

            var user = new UserBuilder()
                .SetEmail(email)
                .SetUsername(username)
                .SetPassword(saltedPasswordHash, passwordSalt)
                .Build();

            database.UserRepository.Add(user);

            if (await database.Complete())
            {
                var registerToken = Token.Create(TokenType.Register);

                user.Tokens.Add(registerToken);

                if (rolesService.AdmitRole(await rolesService.GetRoleId(RoleName.User), user))
                    return await database.Complete() ? new AuthResult(registerToken.Code, user) : null;

                return null;
            }

            return null;
        }

        public async Task<bool> ConfirmAccount(string userId, string token)
        {
            var user = await database.UserRepository.Get(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            if (user.IsBlocked)
                throw new BlockException("Your account is blocked");

            var registerToken = user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.Register)
                ?? throw new TokenException("Token is invalid");

            user.ConfirmAccount();

            user.Tokens.Remove(registerToken);

            return await database.Complete();
        }
    }
}