using System.Linq;
using Smile.Core.Application.Dtos.User;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.ServiceUtils
{
    public static class FriendUtils
    {
        public static UserDtoBase SetFriendProperties(UserDtoBase userDto, User user, string currentUserId)
        {
            userDto.IsFriend = IsFriend(user, currentUserId);
            userDto.IsFriendAccepted = IsFriendAccepted(user, currentUserId);
            userDto.IsCurrentUserSender = IsCurrentUserSender(user, currentUserId);

            return userDto;
        }

        #region private

        private static bool IsFriend(User user, string currentUserId)
                  => user.FriendsSent.Concat(user.FriendsReceived).Any(f => f.SenderId == currentUserId || f.RecipientId == currentUserId);

        private static bool IsFriendAccepted(User user, string currentUserId)
            => user.FriendsSent.Concat(user.FriendsReceived).Any(f => (f.SenderId == currentUserId || f.RecipientId == currentUserId)
                && (f.SenderAccepted && f.RecipientAccepted));

        private static bool IsCurrentUserSender(User user, string currentUserId)
            => user.FriendsSent.Concat(user.FriendsReceived).Any(f => f.SenderId == currentUserId);

        #endregion
    }
}