using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Post;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class DeletePostSpecification : Specification<Post>
    {
        private readonly User user;

        private DeletePostSpecification(User user)
        {
            this.user = user;
        }

        public override Expression<Func<Post, bool>> ToExpression()
            => post => post.AuthorId == user.Id
                    || user.IsAdmin()
                    || (post.Group != null && post.Group.AdminId == user.Id);

        public static DeletePostSpecification Create(User user) => new DeletePostSpecification(user);
    }
}