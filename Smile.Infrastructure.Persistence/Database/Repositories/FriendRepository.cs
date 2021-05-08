using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using Smile.Core.Domain.Entities.Community;
using Smile.Core.Domain.Data.Models;
using System.Threading.Tasks;
using Smile.Core.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        public FriendRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Friend>> GetFilteredFriends(string userId, string friendName, (int PageNumber, int PageSize) pagination)
            => await context.Friends.Where(f => f.SenderId == userId || f.RecipientId == userId)
                .Where(f => string.IsNullOrEmpty(friendName)
                    ? true
                    : (f.SenderId == userId
                        ? f.Recipient.Username.ToLower().Contains(friendName.ToLower())
                        : f.Sender.Username.ToLower().Contains(friendName.ToLower())))
                        .ToPagedList<Friend>(pagination.PageNumber, pagination.PageSize);

        public async Task<int> CountFriendInvites(string userId)
            => await context.Friends
                .Where(f => f.RecipientId == userId && !f.RecipientAccepted)
                .CountAsync();
    }
}