using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class TokenExpirationSpecification : Specification<Token>
    {
        public override Expression<Func<Token, bool>> ToExpression()
            => token => token.DateExpired < DateTime.Now;

        public static TokenExpirationSpecification Create() => new TokenExpirationSpecification();
    }
}