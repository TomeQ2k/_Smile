using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Signalr;
using Smile.Core.Application.Features.Responses.Command.Signalr;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Features.Handlers.Command.Signalr
{
    public class CloseConnectionCommand : IRequestHandler<CloseConnectionRequest, CloseConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;

        public CloseConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public async Task<CloseConnectionResponse> Handle(CloseConnectionRequest request, CancellationToken cancellationToken)
            => await connectionManager.CloseConnection(request.ConnectionId) ? new CloseConnectionResponse()
                : throw new HubConnectionException("Close hub connection failed");
    }
}