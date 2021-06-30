using System.Threading.Tasks;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Services
{
    public interface IAdminService
    {
        Task<bool> AdmitRole(string userId, RoleName role);
        Task<bool> RevokeRole(string userId, RoleName role);

        Task<bool> DeleteUser(string userId);
        Task<bool> BlockAccount(string userId);
        Task<bool> ConfirmAccount(string userId);
    }
}