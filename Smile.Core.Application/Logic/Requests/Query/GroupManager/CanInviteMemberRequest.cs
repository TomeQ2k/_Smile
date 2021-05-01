using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.GroupManager;

namespace Smile.Core.Application.Logic.Requests.Query.GroupManager
{
    public class CanInviteMemberRequest : IRequest<CanInviteMemberResponse>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string GroupId { get; set; }
    }
}