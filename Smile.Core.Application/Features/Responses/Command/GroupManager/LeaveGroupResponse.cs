using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.GroupManager
{
    public class LeaveGroupResponse : BaseResponse
    {
        public LeaveGroupResponse(Error error = null) : base(error) { }
    }
}