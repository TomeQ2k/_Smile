using System.Threading.Tasks;

namespace Smile.Core.Application.Services
{
    public interface IAuthValidationService
    {
        Task<bool> EmailExists(string email);
        Task<bool> UsernameExists(string username);
    }
}