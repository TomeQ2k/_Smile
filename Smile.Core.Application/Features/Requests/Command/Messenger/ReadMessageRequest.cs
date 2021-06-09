using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Messenger;

namespace Smile.Core.Application.Features.Requests.Command.Messenger
{
    public class ReadMessageRequest : IRequest<ReadMessageResponse>
    {
        [Required]
        public string MessageId { get; set; }
    }
}