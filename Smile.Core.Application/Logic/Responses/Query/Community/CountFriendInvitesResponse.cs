using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Community
{
    public class CountFriendInvitesResponse : BaseResponse
    {
        public int FriendInvitesCount { get; set; }

        public CountFriendInvitesResponse(Error error = null) : base(error) { }
    }
}