using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Messenger;

namespace Smile.Core.Application.Logic.Requests.Command.Messenger
{
    public class DeleteConversationRequest : IRequest<DeleteConversationResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}