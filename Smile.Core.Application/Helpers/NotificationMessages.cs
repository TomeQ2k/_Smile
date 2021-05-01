namespace Smile.Core.Application.Helpers
{
    public static class NotificationMessages
    {
        public static string GroupJoinedNotification(string groupName) => $"You joined group: {groupName}";
        public static string NewCommentNotification(string username, string postTitle) => $"{username} commented your post: {postTitle}";
        public static string NewGroupPostNotification(string username, string groupName) => $"{username} added new post in group: {groupName}";
        public static string FriendAcceptedNotification(string friendName) => $"{friendName} accepted your friend request";
        public static string GroupInvitedNotification(string groupName) => $"You were invited to group: {groupName}";
        public static string GroupInviteAcceptedNotification(string groupName) => $"Your join request to group: {groupName} was accepted";
        public static string GroupInviteDeniedNotification(string groupName) => $"Your join request to group: {groupName} was denied";
        public static string MemberKickedNotification(string groupName) => $"You were kicked from group: {groupName}";
        public static string GroupModeratorGrantedNotification(string groupName) => $"You were granted with MODERATOR rank in group: {groupName}";
        public static string GroupModeratorRevokedNotification(string groupName) => $"You were revoked from MODERATOR rank in group: {groupName}";
        public const string AdminGrantedNotification = "You were granted with ADMIN rank";
        public const string AdminRevokedNotification = "You were revoked from ADMIN rank";
        public static string SupportReplyNotification(string reportTitle) => $"You have new reply to report: {reportTitle}";
    }
}