using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Group;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Group
{
    public class JoinGroupRequest : IRequest<JoinGroupResponse>
    {
        [Required]
        public string GroupId { get; set; }

        [StringLength(Constants.GroupCodeLength)]
        public string JoinCode { get; set; }
    }
}