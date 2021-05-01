using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Smile.Core.Application.SignalR;

namespace Smile.Infrastructure.Shared.SignalR
{
    public class HubManager : IHubManager
    {
        private readonly IHubContext<HubClient> hubContext;
        private readonly IConnectionManager connectionManager;

        public HubManager(IHubContext<HubClient> hubContext, IConnectionManager connectionManager)
        {
            this.hubContext = hubContext;
            this.connectionManager = connectionManager;
        }

        public async Task Invoke(string actionName, string clientId, params object[] values)
        {
            string connectionId = await connectionManager.GetConnectionId(clientId);

            if (!string.IsNullOrEmpty(connectionId))
                await hubContext.Clients.Client(await connectionManager.GetConnectionId(clientId)).SendAsync(actionName, values);
        }

        public async Task InvokeToAll(string actionName, params object[] values)
            => await hubContext.Clients.All.SendAsync(actionName, values);
    }
}