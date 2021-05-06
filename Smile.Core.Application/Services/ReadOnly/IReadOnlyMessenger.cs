using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Messenger;
using Smile.Core.Application.Models.Conversation;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Messenger;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyMessenger
    {
        Task<PagedList<Conversation>> GetConversations(GetConversationsPaginationRequest paginationRequest);
        Task<IPagedList<Message>> GetMessagesThread(GetMessagesThreadPaginationRequest paginationRequest);

        Task<User> GetRecipient(string recipientId);

        Task<int> CountUnreadMessages();
    }
}