using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.GroupManager;

namespace Smile.Core.Application.Features.Requests.Command.GroupManager
{
    public class DeleteGroupRequest : IRequest<DeleteGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }
    }
}