using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Messenger;

namespace Smile.Core.Application.Logic.Requests.Command.Messenger
{
    public class DeleteMessageRequest : IRequest<DeleteMessageResponse>
    {
        [Required]
        public string MessageId { get; set; }

        public string RecipientId { get; set; }
    }
}