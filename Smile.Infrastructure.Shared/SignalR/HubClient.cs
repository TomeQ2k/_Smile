using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Smile.Core.Application.SignalR;

namespace Smile.Infrastructure.Shared.SignalR
{
    public class HubClient : Hub
    {
        private readonly IConnectionManager connectionManager;

        public HubClient(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public string GetConnectionId() => Context.ConnectionId;

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await connectionManager.CloseConnection(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}