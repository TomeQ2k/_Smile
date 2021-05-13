using Smile.Core.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Builders;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logic.Requests.Query.Messenger;
using Smile.Core.Application.Models.Conversation;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Messenger;
using Smile.Core.Domain.Data.Models;
using Smile.Infrastructure.Shared.Specifications;

namespace Smile.Infrastructure.Shared.Services
{
    public class Messenger : IMessenger
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IHttpContextReader httpContextReader;

        public Messenger(IDatabase database, IReadOnlyProfileService profileService, IHttpContextReader httpContextReader)
        {
            this.database = database;
            this.profileService = profileService;
            this.httpContextReader = httpContextReader;
        }

        public async Task<PagedList<Conversation>> GetConversations(GetConversationsPaginationRequest paginationRequest)
        {
            var sender = await profileService.GetCurrentUser();
            var senderFriends = sender.FriendsSent.Concat(sender.FriendsReceived);

            var conversations = sender.MessagesSent.Concat(sender.MessagesReceived)
                .Where(m => string.IsNullOrEmpty(paginationRequest.Username) ? true
                    : (m.SenderId == sender.Id ? m.Recipient.Username.ToLower().Contains(paginationRequest.Username.ToLower())
                    : m.Sender.Username.ToLower().Contains(paginationRequest.Username.ToLower())))
                .OrderByDescending(m => m.DateSent)
                .GroupBy(m => new { m.SenderId, m.RecipientId })
                .Select(g =>
                {
                    var message = g.First();

                    var conversation = new ConversationBuilder()
                        .SentBy(message.SenderId)
                        .SentTo(message.RecipientId)
                        .SetLastMessage(new LastMessageBuilder()
                            .SetText(message.Text)
                            .SentBy(message.SenderId, message.Sender.Username, message.Sender.PhotoUrl)
                            .Sent(message.DateSent)
                            .MarkAsRead(message.IsRead)
                            .Build())
                        .SetUserData(sender.Id == message.SenderId ? message.Recipient.Username : message.Sender.Username,
                            sender.Id == message.SenderId ? message.Recipient.PhotoUrl : message.Sender.PhotoUrl)
                        .Build();

                    return conversation;
                })
                .Where(c => senderFriends.Any(f => (f.SenderAccepted && f.RecipientAccepted) && (f.SenderId == c.SenderId || f.SenderId == c.RecipientId) &&
                        (f.RecipientId == c.SenderId || f.RecipientId == c.RecipientId)))
                .ToList();

            var uniqueConversations = new List<Conversation>();

            conversations.ForEach(c =>
            {
                if (uniqueConversations.Count == 0 || !uniqueConversations.Any(uc => (uc.SenderId == c.SenderId && uc.RecipientId == c.RecipientId)
                    || (uc.SenderId == c.RecipientId && uc.RecipientId == c.SenderId)))
                    uniqueConversations.Add(c);
            });

            return uniqueConversations.ToPagedList<Conversation>(paginationRequest.PageNumber, paginationRequest.PageSize);
        }

        public async Task<IPagedList<Message>> GetMessagesThread(GetMessagesThreadPaginationRequest paginationRequest)
        {
            var currentUser = await profileService.GetCurrentUser();

            if (currentUser.Id == paginationRequest.RecipientId)
                throw new EntityNotFoundException("Messages thread not found");

            if (!SendMessageOnlyToFriendsSpecification.Create(paginationRequest.RecipientId).IsSatisfied(currentUser))
                throw new NoPermissionsException("You can chat only with your friends");

            var messages = await database.MessageRepository.GetMessagesThread(currentUser.Id, paginationRequest.RecipientId, (paginationRequest.PageNumber, paginationRequest.PageSize));

            messages = await MarkAsRead(currentUser.Id, paginationRequest.RecipientId, messages);

            return messages;
        }

        public async Task<Message> Send(string recipientId, string text)
        {
            var sender = await profileService.GetCurrentUser();

            if (sender.Id == recipientId)
                throw new NoPermissionsException("You cannot send message to yourself");

            if (!SendMessageOnlyToFriendsSpecification.Create(recipientId).IsSatisfied(sender))
                throw new NoPermissionsException("You can send message only to your friends");

            var recipient = await GetRecipient(recipientId);

            var message = Message.Create(text);

            sender.MessagesSent.Add(message);
            recipient.MessagesReceived.Add(message);

            return await database.Complete() ? message : null;
        }

        public async Task<bool> Delete(string messageId)
        {
            var sender = await profileService.GetCurrentUser();
            var message = sender.MessagesSent.FirstOrDefault(m => m.Id == messageId) ?? throw new EntityNotFoundException("Message not found");

            database.MessageRepository.Delete(message);

            return await database.Complete();
        }

        public async Task<bool> DeleteConversation(string recipientId)
        {
            var currentUser = await profileService.GetCurrentUser();

            var conversationMessages = currentUser.MessagesSent.Concat(currentUser.MessagesReceived)
                .Where(m => (m.SenderId == currentUser.Id && m.RecipientId == recipientId) || (m.SenderId == recipientId && m.RecipientId == currentUser.Id));

            database.MessageRepository.DeleteRange(conversationMessages);

            return await database.Complete();
        }

        public async Task<bool> ReadMessage(string messageId)
        {
            var messageToRead = await database.MessageRepository.Get(messageId) ?? throw new EntityNotFoundException("Message not found");

            messageToRead.MarkAsRead();

            database.MessageRepository.Update(messageToRead);

            return await database.Complete();
        }

        public async Task<User> GetRecipient(string recipientId) => await database.UserRepository.Get(recipientId) ?? throw new EntityNotFoundException("Recipient not found");

        public async Task<int> CountUnreadMessages()
            => await database.MessageRepository.CountUnreadMessages(httpContextReader.CurrentUserId);

        #region private

        private async Task<IPagedList<Message>> MarkAsRead(string currentUserId, string recipientId, IPagedList<Message> userMessages)
        {
            if ((userMessages.FirstOrDefault())?.RecipientId != currentUserId)
                return userMessages;

            userMessages.TakeWhile(m => !m.IsRead).ToList().ForEach(m => m.MarkAsRead());

            await database.Complete();

            return userMessages;
        }

        #endregion
    }
}