using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smile.Core.Application.Extensions;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Data.Repositories;
using Smile.Core.Domain.Entities.Messenger;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Message>> GetMessagesThread(string userId, string recipientId, (int PageNumber, int PageSize) pagination)
            => await context.Messages
                .Where(m => (m.SenderId == userId && m.RecipientId == recipientId) || (m.SenderId == recipientId && m.RecipientId == userId))
                .OrderByDescending(m => m.DateSent)
                .ToPagedList<Message>(pagination.PageNumber, pagination.PageSize);

        public async Task<int> CountUnreadMessages(string userId)
            => await context.Messages
                .Where(m => m.RecipientId == userId && !m.IsRead)
                .OrderByDescending(m => m.DateSent)
                .GroupBy(m => new { m.SenderId })
                .CountAsync();
    }
}