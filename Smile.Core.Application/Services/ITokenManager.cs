using System.Threading.Tasks;

namespace Smile.Core.Application.Services
{
    public interface ITokenManager
    {
        Task ClearExpiredTokens();
    }
}