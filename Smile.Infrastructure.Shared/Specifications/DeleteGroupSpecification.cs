using System;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class DeleteGroupSpecification : Specification<Group>
    {
        private readonly User currentUser;

        private DeleteGroupSpecification(User currentUser)
        {
            this.currentUser = currentUser;
        }

        public override Expression<Func<Group, bool>> ToExpression()
            => group => group.AdminId == currentUser.Id || currentUser.IsAdmin();

        public static DeleteGroupSpecification Create(User currentUser) => new DeleteGroupSpecification(currentUser);
    }
}