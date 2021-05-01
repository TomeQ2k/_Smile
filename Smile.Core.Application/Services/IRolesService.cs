using System.Threading.Tasks;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services
{
    public interface IRolesService : IReadOnlyRolesService
    {
        bool AdmitRole(string roleId, User user);
        bool RevokeRole(string roleId, User user);

        Task<bool> CreateRole(RoleName roleName);
    }
}