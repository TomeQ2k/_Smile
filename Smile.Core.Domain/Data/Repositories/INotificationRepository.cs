using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Notification;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetOrderedNotifications(string currentUserId);
    }
}