using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Messenger;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Messenger
{
    public class SendMessageRequest : IRequest<SendMessageResponse>
    {
        [Required]
        public string RecipientId { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MessageLength)]
        public string Text { get; set; }
    }
}