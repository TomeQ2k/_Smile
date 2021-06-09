using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Signalr;
using Smile.Core.Application.Features.Responses.Command.Signalr;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Features.Handlers.Command.Signalr
{
    public class StartConnectionCommand : IRequestHandler<StartConnectionRequest, StartConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;

        public StartConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public async Task<StartConnectionResponse> Handle(StartConnectionRequest request, CancellationToken cancellationToken)
            => await connectionManager.StartConnection(request.ConnectionId) ? new StartConnectionResponse()
                : throw new HubConnectionException("Start hub connection failed");
    }
}