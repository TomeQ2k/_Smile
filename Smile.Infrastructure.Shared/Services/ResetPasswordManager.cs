using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Entities.Auth;
using Smile.Infrastructure.Shared.Specifications;

namespace Smile.Infrastructure.Shared.Services
{
    public class ResetPasswordManager : IResetPasswordManager
    {
        private readonly IDatabase database;
        private readonly IHashGenerator hashGenerator;

        public ResetPasswordManager(IDatabase database, IHashGenerator hashGenerator)
        {
            this.database = database;
            this.hashGenerator = hashGenerator;
        }

        public async Task<bool> ResetPassword(string userId, string token, string newPassword)
        {
            var user = await database.UserRepository.FindById(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException("Your account is blocked");

            var resetPasswordToken = user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.ResetPassword)
                ?? throw new TokenException("Token is invalid");

            if (TokenExpirationSpecification.Create().IsSatisfied(resetPasswordToken))
                throw new TokenException("Token expired", ErrorCodes.TokenExpired);

            string saltedPasswordHash = string.Empty;
            var passwordSalt = hashGenerator.CreateSalt();

            hashGenerator.GenerateHash(newPassword, passwordSalt, out saltedPasswordHash);

            user.SetPassword(saltedPasswordHash, passwordSalt);

            if (await database.Complete())
            {
                user.Tokens.Remove(resetPasswordToken);

                return await database.Complete();
            }

            return false;
        }

        public async Task<SendResetPasswordResult> GenerateResetPasswordToken(string email)
        {
            var user = await database.UserRepository.Find(u => u.Email.ToLower() == email.ToLower()) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException("Your account is blocked");

            var resetPasswordToken = Token.Create(TokenType.ResetPassword);
            resetPasswordToken.SetExpirationDate(new TimeSpan(Constants.TokenExpirationTimeInHours, 0, 0));

            user.Tokens.Add(resetPasswordToken);

            return await database.Complete()
                ? new SendResetPasswordResult(resetPasswordToken.Code, user.Id, user.Username, email)
                : null;
        }

        public async Task<bool> VerifyResetPasswordToken(string userId, string token)
        {
            var user = await database.UserRepository.FindById(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            if (UserBlockedSpecification.Create().IsSatisfied(user))
                throw new BlockException("Your account is blocked");

            var resetPasswordToken = user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.ResetPassword)
                ?? throw new TokenException("Token is invalid");

            if (TokenExpirationSpecification.Create().IsSatisfied(resetPasswordToken))
                throw new TokenException("Token expired", ErrorCodes.TokenExpired);

            return true;
        }
    }
}