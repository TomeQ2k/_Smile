using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.Group;

namespace Smile.Core.Application.Features.Requests.Query.Group
{
    public class GetGroupRequest : IRequest<GetGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }
    }
}