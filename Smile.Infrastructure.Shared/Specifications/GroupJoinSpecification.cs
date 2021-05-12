using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class GroupJoinSpecification : Specification<Group>
    {
        private readonly string joinCode;

        private GroupJoinSpecification(string joinCode)
        {
            this.joinCode = joinCode;
        }

        public override Expression<Func<Group, bool>> ToExpression()
            => group => !group.IsPrivate ||
                        (group.IsPrivate && !string.IsNullOrEmpty(group.JoinCode) && group.JoinCode == joinCode);

        public static GroupJoinSpecification Create(string joinCode) => new GroupJoinSpecification(joinCode);
    }
}