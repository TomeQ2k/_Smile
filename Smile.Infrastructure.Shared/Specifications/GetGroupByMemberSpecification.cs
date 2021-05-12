using System;
using System.Linq;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class GetGroupByMemberSpecification : Specification<Group>
    {
        private readonly User currentUser;

        private GetGroupByMemberSpecification(User currentUser)
        {
            this.currentUser = currentUser;
        }

        public override Expression<Func<Group, bool>> ToExpression()
            => group => group.AdminId == currentUser.Id
                        || group.GroupMembers.Any(m => m.UserId == currentUser.Id && m.IsAccepted)
                        || currentUser.IsAdmin();

        public static GetGroupByMemberSpecification Create(User currentUser) => new GetGroupByMemberSpecification(currentUser);
    }
}