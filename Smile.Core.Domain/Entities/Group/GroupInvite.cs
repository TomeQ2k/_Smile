using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Group
{
    public class GroupInvite
    {
        public string UserId { get; protected set; }
        public string GroupId { get; protected set; }
        public bool IsJoining { get; protected set; }
        public bool IsInvited { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Group Group { get; protected set; }

        public static GroupInvite Create(string userId, string groupId, bool isInvited) => new GroupInvite
        {
            UserId = userId,
            GroupId = groupId,
            IsJoining = !isInvited,
            IsInvited = isInvited
        };
    }
}