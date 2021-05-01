using System;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Group
{
    public class GroupMember
    {
        public string UserId { get; protected set; }
        public string GroupId { get; protected set; }
        public bool IsAccepted { get; protected set; }
        public bool IsModerator { get; protected set; }
        public DateTime DateJoined { get; protected set; } = DateTime.Now;

        public virtual User User { get; protected set; }
        public virtual Group Group { get; protected set; }

        public static GroupMember Create(string userId, string groupId) => new GroupMember
        {
            UserId = userId,
            GroupId = groupId
        };

        public void Accept()
        {
            IsAccepted = true;
        }

        public void SetIsModerator(bool isModerator)
        {
            IsModerator = isModerator;
        }
    }
}