using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        public FriendRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Friend> GetFilteredFriends(string userId, string friendName)
            => context.Friends.Where(f => f.SenderId == userId || f.RecipientId == userId)
                .Where(f => string.IsNullOrEmpty(friendName)
                    ? true
                    : (f.SenderId == userId
                        ? f.Recipient.Username.ToLower().Contains(friendName.ToLower())
                        : f.Sender.Username.ToLower().Contains(friendName.ToLower())));
    }
}