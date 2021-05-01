using System;
using Smile.Core.Application.Models.Conversation;

namespace Smile.Core.Application.Builders.Interfaces
{
    public interface ILastMessageBuilder : IBuilder<LastMessage>
    {
        ILastMessageBuilder SetText(string text);
        ILastMessageBuilder SentBy(string senderId, string senderName, string senderPhotoUrl);
        ILastMessageBuilder Sent(DateTime dateSent);
        ILastMessageBuilder MarkAsRead(bool isRead);
    }
}