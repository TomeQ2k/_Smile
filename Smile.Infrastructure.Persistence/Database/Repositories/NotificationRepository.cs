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

        public async Task<IEnumerable<Notification>> GetOrderedNotifications(string currentUserId)
            => await context.Notifications.Where(n => n.UserId == currentUserId)
                .OrderByDescending(n => n.DateSent)
                .ToListAsync();
    }
}