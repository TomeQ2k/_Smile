using System.Threading.Tasks;

namespace Smile.Core.Application.Services
{
    public interface IAdminService
    {
        Task<bool> AdmitRole(string userId, string roleId);
        Task<bool> RevokeRole(string userId, string roleId);

        Task<bool> DeleteUser(string userId);
        Task<bool> BlockAccount(string userId);
        Task<bool> ConfirmAccount(string userId);
    }
}