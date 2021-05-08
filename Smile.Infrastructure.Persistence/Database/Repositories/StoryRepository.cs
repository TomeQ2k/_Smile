using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smile.Core.Domain.Data.Repositories;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        public StoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Story>> GetUserCurrentStories(string userId)
        {
            var userFriends = context.Friends.Where(f => (f.SenderId == userId || f.RecipientId == userId)
                            && (f.SenderAccepted && f.RecipientAccepted));

            return await context.Stories
                .Where(s => s.DateExpires >= DateTime.Now
                    && userFriends.Any(f => (f.SenderId == s.UserId || f.RecipientId == s.UserId)
                    && (f.SenderAccepted && f.RecipientAccepted)))
                .OrderByDescending(s => s.DateExpires)
                .ToListAsync();
        }
    }
}