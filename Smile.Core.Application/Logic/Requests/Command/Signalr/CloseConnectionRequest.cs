using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Signalr;

namespace Smile.Core.Application.Logic.Requests.Command.Signalr
{
    public class CloseConnectionRequest : IRequest<CloseConnectionResponse>
    {
        [Required]
        public string ConnectionId { get; set; }
    }
}