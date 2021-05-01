using System.Threading.Tasks;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Services
{
    public interface INotifier : IReadOnlyNotifier
    {
        Task Push(string message, NotificationType type = NotificationType.Other);
        Task Push(string message, string userId, NotificationType type = NotificationType.Other);

        Task<bool> MarkAsRead();

        Task<bool> Remove(string notificationId);
        Task<bool> Clear();
    }
}