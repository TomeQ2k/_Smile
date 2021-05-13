using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class UserConfirmedSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
            => user => user.EmailConfirmed;

        public static UserConfirmedSpecification Create() => new UserConfirmedSpecification();
    }
}