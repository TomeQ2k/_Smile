using System;
using System.Linq.Expressions;
using Smile.Core.Application.Services;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class CanUserBeManagedSpecification : Specification<User>
    {
        private readonly User currentAdmin;
        private readonly IRolesService rolesService;

        private CanUserBeManagedSpecification(User currentAdmin, IRolesService rolesService)
        {
            this.currentAdmin = currentAdmin;
            this.rolesService = rolesService;
        }

        public override Expression<Func<User, bool>> ToExpression()
            => user => !user.IsAdmin() || rolesService.IsPermitted(currentAdmin, RoleName.HeadAdmin);

        public static CanUserBeManagedSpecification Create(User currentAdmin, IRolesService rolesService)
            => new CanUserBeManagedSpecification(currentAdmin, rolesService);
    }
}