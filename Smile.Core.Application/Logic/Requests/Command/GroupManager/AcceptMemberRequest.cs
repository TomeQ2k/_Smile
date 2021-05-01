using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;

namespace Smile.Core.Application.Logic.Requests.Command.GroupManager
{
    public class AcceptMemberRequest : IRequest<AcceptMemberResponse>
    {
        [Required]
        public string GroupId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool Accept { get; set; } = true;
    }
}