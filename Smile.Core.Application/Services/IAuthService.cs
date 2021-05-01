using System.Threading.Tasks;
using Smile.Core.Application.Results;

namespace Smile.Core.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> SignIn(string email, string password);
        Task<AuthResult> SignUp(string email, string password, string username);

        Task<bool> ConfirmAccount(string userId, string token);
    }
}