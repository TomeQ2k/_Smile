using System;
using System.Linq;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class IsUserGroupMemberSpecification : Specification<Group>
    {
        private readonly string userId;

        private IsUserGroupMemberSpecification(string userId)
        {
            this.userId = userId;
        }

        public override Expression<Func<Group, bool>> ToExpression()
            => group => group.AdminId == userId
                        || group.GroupMembers.Any(m => m.UserId == userId);

        public static IsUserGroupMemberSpecification Create(string userId) => new IsUserGroupMemberSpecification(userId);
    }
}