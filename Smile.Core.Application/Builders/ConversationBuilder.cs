using Smile.Core.Application.Builders.Interfaces;
using Smile.Core.Application.Models.Conversation;

namespace Smile.Core.Application.Builders
{
    public class ConversationBuilder : IConversationBuilder
    {
        private readonly Conversation conversation = new Conversation();

        public IConversationBuilder SentBy(string senderId)
        {
            this.conversation.SenderId = senderId;

            return this;
        }

        public IConversationBuilder SentTo(string recipientId)
        {
            this.conversation.RecipientId = recipientId;

            return this;
        }

        public IConversationBuilder SetLastMessage(LastMessage lastMessage)
        {
            this.conversation.LastMessage = lastMessage;

            return this;
        }

        public IConversationBuilder SetUserData(string username, string avatarUrl)
        {
            this.conversation.Username = username;
            this.conversation.AvatarUrl = avatarUrl;

            return this;
        }

        public Conversation Build() => this.conversation;
    }
}