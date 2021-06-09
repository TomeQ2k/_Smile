using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Messenger;

namespace Smile.Core.Application.Features.Requests.Command.Messenger
{
    public class DeleteConversationRequest : IRequest<DeleteConversationResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}