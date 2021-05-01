using Smile.Core.Domain.Entities.Community;

namespace Smile.Core.Application.Results
{
    public class ReceiveResult
    {
        public bool FriendAccepted { get; }
        public Friend Friend { get; }

        public ReceiveResult(Friend friend = null, bool friendAccepted = true)
        {
            Friend = friend;
            FriendAccepted = friendAccepted;
        }
    }
}