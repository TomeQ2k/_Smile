using System.Threading.Tasks;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services
{
    public interface IRolesService : IReadOnlyRolesService
    {
        Task<bool> AdmitRole(RoleName roleName, User user);
        bool AdmitRole(string roleId, User user);

        Task<bool> RevokeRole(RoleName roleName, User user);
        bool RevokeRole(string roleId, User user);

        Task<bool> CreateRole(RoleName roleName);
    }
}