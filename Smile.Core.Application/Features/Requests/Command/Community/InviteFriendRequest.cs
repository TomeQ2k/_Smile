using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Community;

namespace Smile.Core.Application.Features.Requests.Command.Community
{
    public class InviteFriendRequest : IRequest<InviteFriendResponse>
    {
        [Required]
        public string RecipientId { get; set; }
    }
}