using System;
using System.Linq;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class CreateGroupPostSpecification : Specification<Post>
    {
        private readonly User user;

        private CreateGroupPostSpecification(User user)
        {
            this.user = user;
        }

        public override Expression<Func<Post, bool>> ToExpression()
            => post => string.IsNullOrEmpty(post.GroupId)
                    || user.Groups.Concat(user.GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group))
                        .Any(g => g.Id == post.GroupId);

        public static CreateGroupPostSpecification Create(User user) => new CreateGroupPostSpecification(user);
    }
}