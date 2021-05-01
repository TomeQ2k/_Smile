using System.Threading.Tasks;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Services
{
    public interface IResetPasswordManager : IReadOnlyResetPasswordManager
    {
        Task<bool> ResetPassword(string userId, string token, string newPassword);
        Task<SendResetPasswordResult> GenerateResetPasswordToken(string email);
    }
}