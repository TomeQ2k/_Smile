using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Signalr;

namespace Smile.Core.Application.Features.Requests.Command.Signalr
{
    public class CloseConnectionRequest : IRequest<CloseConnectionResponse>
    {
        [Required]
        public string ConnectionId { get; set; }
    }
}