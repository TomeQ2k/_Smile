using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Post;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class UpdatePostSpecification : Specification<Post>
    {
        private readonly User user;

        private UpdatePostSpecification(User user)
        {
            this.user = user;
        }

        public override Expression<Func<Post, bool>> ToExpression()
            => post => post.AuthorId == user.Id
                    || user.IsAdmin();

        public static UpdatePostSpecification Create(User user) => new UpdatePostSpecification(user);
    }
}