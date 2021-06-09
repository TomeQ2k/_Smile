using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Group
{
    public class JoinGroupResponse : BaseResponse
    {
        public GroupMemberDto Member { get; set; }

        public JoinGroupResponse(Error error = null) : base(error) { }
    }
}