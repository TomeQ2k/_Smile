using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Community
{
    public class InviteFriendResponse : BaseResponse
    {
        public FriendDto Friend { get; set; }

        public InviteFriendResponse(Error error = null) : base(error) { }
    }
}