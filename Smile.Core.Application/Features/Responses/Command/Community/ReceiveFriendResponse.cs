using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Community
{
    public class ReceiveFriendResponse : BaseResponse
    {
        public bool FriendAccepted { get; set; }
        public FriendDto Friend { get; set; }

        public ReceiveFriendResponse(Error error = null) : base(error) { }
    }
}