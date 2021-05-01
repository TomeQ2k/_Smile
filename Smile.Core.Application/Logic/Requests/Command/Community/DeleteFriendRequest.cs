using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Community;

namespace Smile.Core.Application.Logic.Requests.Command.Community
{
    public class DeleteFriendRequest : IRequest<DeleteFriendResponse>
    {
        [Required]
        public string FriendId { get; set; }
    }
}