using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class InvalidGroupJoinCodeSpecification : Specification<Group>
    {
        private readonly string joinCode;

        private InvalidGroupJoinCodeSpecification(string joinCode)
        {
            this.joinCode = joinCode;
        }

        public override Expression<Func<Group, bool>> ToExpression()
            => group => group.IsPrivate
                        && !string.IsNullOrEmpty(group.JoinCode) && group.JoinCode != joinCode;

        public static InvalidGroupJoinCodeSpecification Create(string joinCode) => new InvalidGroupJoinCodeSpecification(joinCode);
    }
}