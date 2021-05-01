using Smile.Core.Application.Models.Conversation;

namespace Smile.Core.Application.Builders.Interfaces
{
    public interface IConversationBuilder : IBuilder<Conversation>
    {
        IConversationBuilder SentBy(string senderId);
        IConversationBuilder SentTo(string recipientId);
        IConversationBuilder SetLastMessage(LastMessage lastMessage);
        IConversationBuilder SetUserData(string username, string avatarUrl);
    }
}