using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Comment;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class UpdateOrDeleteCommentSpecification : Specification<Comment>
    {
        private readonly User user;

        private UpdateOrDeleteCommentSpecification(User user)
        {
            this.user = user;
        }

        public override Expression<Func<Comment, bool>> ToExpression()
            => comment => comment.UserId == user.Id || user.IsAdmin();

        public static UpdateOrDeleteCommentSpecification Create(User user) => new UpdateOrDeleteCommentSpecification(user);
    }
}