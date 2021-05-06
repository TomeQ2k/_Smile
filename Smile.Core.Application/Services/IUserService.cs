using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.User;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string userId, string currentUserId = null);
        Task<IPagedList<User>> GetUsers(GetUsersPaginationRequest paginationRequest);
    }
}