using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Community;

namespace Smile.Core.Application.Logic.Requests.Command.Community
{
    public class ReceiveFriendRequest : IRequest<ReceiveFriendResponse>
    {
        [Required]
        public string SenderId { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public bool Accepted { get; set; } = true;
    }
}