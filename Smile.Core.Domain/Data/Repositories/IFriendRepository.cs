using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IFriendRepository : IRepository<Friend>
    {
        Task<IPagedList<Friend>> GetFilteredFriends(string userId, string friendName, (int PageNumber, int PageSize) pagination);

        Task<int> CountFriendInvites(string userId);
    }
}