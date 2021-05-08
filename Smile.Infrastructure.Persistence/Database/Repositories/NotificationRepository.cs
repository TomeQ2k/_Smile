using Microsoft.EntityFrameworkCore;
using Smile.Core.Domain.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Notification;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetOrderedNotifications(string userId)
            => await context.Notifications.Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateSent)
                .ToListAsync();

        public async Task<int> CountUnreadNotifications(string userId)
            => await context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .CountAsync();
    }
}