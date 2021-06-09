using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Community
{
    public class DeleteFriendResponse : BaseResponse
    {
        public DeleteFriendResponse(Error error = null) : base(error) { }
    }
}