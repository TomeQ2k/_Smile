using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.GroupManager
{
    public class KickMemberResponse : BaseResponse
    {
        public KickMemberResponse(Error error = null) : base(error) { }
    }
}