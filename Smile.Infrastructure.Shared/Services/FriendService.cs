using Smile.Core.Domain.Data;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Community;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Community;
using Smile.Core.Domain.Data.Models;

namespace Smile.Infrastructure.Shared.Services
{
    public class FriendService : IFriendService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IHttpContextReader httpContextReader;

        public FriendService(IDatabase database, IReadOnlyProfileService profileService, IHttpContextReader httpContextReader)
        {
            this.database = database;
            this.profileService = profileService;
            this.httpContextReader = httpContextReader;
        }

        public async Task<IPagedList<Friend>> GetFriends(GetFriendsPaginationRequest paginationRequest)
            => await database.FriendRepository
                .GetFilteredFriends(paginationRequest.UserId, paginationRequest.FriendName, (paginationRequest.PageNumber, paginationRequest.PageSize));

        public async Task<Friend> Invite(string recipientId)
        {
            var user = await profileService.GetCurrentUser();

            if (user.Id == recipientId)
                return null;

            if (await GetFriend(user.Id, recipientId) != null)
                throw new DuplicateException("You are already friends");

            var recipient = await database.UserRepository.Get(recipientId) ??
                            throw new EntityNotFoundException("Recipient not found");

            var friend = Friend.Create(user.Id, recipientId);

            user.FriendsSent.Add(friend);
            recipient.FriendsReceived.Add(friend);

            return await database.Complete() ? friend : null;
        }

        public async Task<ReceiveResult> Receive(string senderId, string recipientId, bool accepted = true)
        {
            var user = await profileService.GetCurrentUser();

            if (senderId == recipientId)
                return null;

            var friend = await GetFriend(senderId, recipientId) ??
                         throw new EntityNotFoundException("Friend not found");

            if (accepted)
            {
                if (friend.SenderId == user.Id)
                    return null;

                friend.Accept();

                return await database.Complete() ? new ReceiveResult(friend: friend) : null;
            }

            database.FriendRepository.Delete(friend);

            return await database.Complete() ? new ReceiveResult(friendAccepted: false) : null;
        }

        public async Task<bool> DeleteFriend(string friendId)
        {
            var user = await profileService.GetCurrentUser();

            var friend = await GetFriend(user.Id, friendId) ?? throw new EntityNotFoundException("Friend not found");

            database.FriendRepository.Delete(friend);

            return await database.Complete();
        }

        public async Task<int> CountFriendInvites()
            => await database.FriendRepository.CountFriendInvites(httpContextReader.CurrentUserId);

        #region private

        private async Task<Friend> GetFriend(string senderId, string recipientId)
            => await database.FriendRepository.Find(f => (f.SenderId == senderId && f.RecipientId == recipientId)
                                                         || (f.RecipientId == senderId && f.SenderId == recipientId));

        #endregion
    }
}