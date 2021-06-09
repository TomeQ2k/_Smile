using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.GroupManager;

namespace Smile.Core.Application.Features.Requests.Command.GroupManager
{
    public class SetModeratorRequest : IRequest<SetModeratorResponse>
    {
        [Required]
        public string GroupId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool IsModerator { get; set; }
    }
}