using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Group;

namespace Smile.Core.Application.Logic.Requests.Query.Group
{
    public class GetGroupRequest : IRequest<GetGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }
    }
}