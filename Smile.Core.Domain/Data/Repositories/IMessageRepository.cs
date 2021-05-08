using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Messenger;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IPagedList<Message>> GetMessagesThread(string userId, string recipientId, (int PageNumber, int PageSize) pagination);

        Task<int> CountUnreadMessages(string userId);
    }
}