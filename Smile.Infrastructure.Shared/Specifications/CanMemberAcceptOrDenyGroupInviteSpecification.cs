using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class CanMemberAcceptOrDenyGroupInviteSpecification : Specification<GroupInvite>
    {
        private readonly Group group;
        private readonly string currentUserId;
        private readonly string userId;

        private CanMemberAcceptOrDenyGroupInviteSpecification(Group group, string currentUserId, string userId)
        {
            this.group = group;
            this.currentUserId = currentUserId;
            this.userId = userId;
        }

        public override Expression<Func<GroupInvite, bool>> ToExpression()
            => memberInvite => (currentUserId == userId && memberInvite.IsInvited)
                            || (currentUserId == group.AdminId && memberInvite.IsJoining);

        public static CanMemberAcceptOrDenyGroupInviteSpecification Create(Group group, string currentUserId, string userId)
            => new CanMemberAcceptOrDenyGroupInviteSpecification(group, currentUserId, userId);
    }
}