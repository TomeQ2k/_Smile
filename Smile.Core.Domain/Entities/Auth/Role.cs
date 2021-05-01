using System.Collections.Generic;
using Smile.Core.Common.Enums;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Domain.Entities.Auth
{
    public class Role
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Name { get; protected set; }

        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();

        public static Role Create(RoleName roleName) => new Role { Name = Utils.EnumToString<RoleName>(roleName) };
    }
}