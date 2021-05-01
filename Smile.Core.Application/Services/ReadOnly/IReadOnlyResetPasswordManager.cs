using System.Threading.Tasks;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyResetPasswordManager
    {
        Task<bool> VerifyResetPasswordToken(string userId, string token);
    }
}