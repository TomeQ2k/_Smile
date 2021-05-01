using System.Threading.Tasks;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Core.Application.Services
{
    public interface IFriendService : IReadOnlyFriendService
    {
        Task<Friend> Invite(string recipientId);
        Task<ReceiveResult> Receive(string senderId, string recipientId, bool accepted = true);
        Task<bool> DeleteFriend(string friendId);
    }
}