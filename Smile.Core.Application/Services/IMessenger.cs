using System.Threading.Tasks;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Messenger;

namespace Smile.Core.Application.Services
{
    public interface IMessenger : IReadOnlyMessenger
    {
        Task<Message> Send(string recipientId, string text);
        Task<bool> Delete(string messageId);

        Task<bool> DeleteConversation(string recipientId);

        Task<bool> ReadMessage(string messageId);
    }
}