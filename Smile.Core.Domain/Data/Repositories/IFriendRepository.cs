using System.Linq;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IFriendRepository : IRepository<Friend>
    {
        IQueryable<Friend> GetFilteredFriends(string userId, string friendName);
    }
}