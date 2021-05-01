using System.Threading.Tasks;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyRolesService
    {
        bool IsPermitted(User user, params RoleName[] roleNames);
        Task<bool> IsPermitted(string userId, params RoleName[] roleNames);

        Task<bool> RoleExists(RoleName roleName);

        Task<string> GetRoleId(RoleName roleName);
    }
}