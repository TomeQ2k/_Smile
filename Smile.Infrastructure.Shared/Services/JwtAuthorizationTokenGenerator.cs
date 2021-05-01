using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Services
{
    public class JwtAuthorizationTokenGenerator : IJwtAuthorizationTokenGenerator
    {
        private readonly IDatabase database;

        public IConfiguration Configuration { get; }

        private const int TokenExpireTimeInDays = 7;

        public JwtAuthorizationTokenGenerator(IDatabase database, IConfiguration configuration)
        {
            this.database = database;

            Configuration = configuration;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await database.RoleRepository.GetAll();
            var rolesToAdd = roles.Join(user.UserRoles, r => r, ur => ur.Role, (r, ur) => new { RoleName = r.Name });

            foreach (var role in rolesToAdd)
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>(AppSettingsKeys.Token)));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(TokenExpireTimeInDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}