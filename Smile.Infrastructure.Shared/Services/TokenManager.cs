using Smile.Core.Domain.Data;
using System;
using System.Threading.Tasks;
using Smile.Core.Application.Services;

namespace Smile.Infrastructure.Shared.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDatabase database;

        public TokenManager(IDatabase database)
        {
            this.database = database;
        }

        public async Task ClearExpiredTokens()
        {
            var expiredTokens = await database.TokenRepository.GetWhere(t => t.DateExpired != null && t.DateExpired < DateTime.Now);

            database.TokenRepository.DeleteRange(expiredTokens);

            await database.Complete();
        }
    }
}