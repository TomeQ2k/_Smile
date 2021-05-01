using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services
{
    public interface IAuthorizationTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}