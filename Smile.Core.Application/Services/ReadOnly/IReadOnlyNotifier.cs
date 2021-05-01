using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Notification;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyNotifier
    {
        Task<IEnumerable<Notification>> FetchNotifications();

        Task<int> CountUnreadNotifications();
    }
}