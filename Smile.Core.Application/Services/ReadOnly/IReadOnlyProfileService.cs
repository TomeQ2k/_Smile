using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyProfileService
    {
        Task<User> GetCurrentUser();
    }
}