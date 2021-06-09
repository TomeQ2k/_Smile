using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.GroupManager
{
    public class AcceptMemberResponse : BaseResponse
    {
        public GroupMemberDto Member { get; set; }

        public AcceptMemberResponse(Error error = null) : base(error) { }
    }
}