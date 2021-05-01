using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;
using Smile.Core.Domain.Entities.Connection;

namespace Smile.Infrastructure.Shared.SignalR
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IDatabase database;
        private readonly IHttpContextReader httpContextReader;

        public ConnectionManager(IDatabase database, IHttpContextReader httpContextReader)
        {
            this.database = database;
            this.httpContextReader = httpContextReader;
        }

        public async Task<bool> StartConnection(string connectionId)
        {
            Connection connection = default(Connection);

            connection = await database.ConnectionRepository.Find(c => c.UserId == httpContextReader.CurrentUserId);

            if (connection != null)
                database.ConnectionRepository.Delete(connection);

            connection = Connection.Create(httpContextReader.CurrentUserId, connectionId);
            database.ConnectionRepository.Add(connection);

            return await database.Complete();
        }

        public async Task<bool> CloseConnection(string connectionId)
        {
            var connection = await database.ConnectionRepository.Find(c => c.ConnectionId == connectionId);

            if (connection != null)
            {
                database.ConnectionRepository.Delete(connection);

                return await database.Complete();
            }

            return true;
        }

        public async Task<string> GetConnectionId(string userId) =>
            (await database.ConnectionRepository.Find(c => c.UserId == userId))?.ConnectionId;
    }
}