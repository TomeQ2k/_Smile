using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class UserBlockedSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
            => user => user.IsBlocked;

        public static UserBlockedSpecification Create() => new UserBlockedSpecification();
    }
}