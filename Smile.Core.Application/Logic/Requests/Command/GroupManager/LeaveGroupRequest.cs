using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.GroupManager;

namespace Smile.Core.Application.Logic.Requests.Command.GroupManager
{
    public class LeaveGroupRequest : IRequest<LeaveGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }
    }
}