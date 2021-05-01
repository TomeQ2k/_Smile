using System;
using Smile.Core.Application.Builders.Interfaces;
using Smile.Core.Application.Models.Conversation;

namespace Smile.Core.Application.Builders
{
    public class LastMessageBuilder : ILastMessageBuilder
    {
        private readonly LastMessage lastMessage = new LastMessage();

        public ILastMessageBuilder SetText(string text)
        {
            this.lastMessage.Text = text;

            return this;
        }

        public ILastMessageBuilder SentBy(string senderId, string senderName, string senderPhotoUrl)
        {
            this.lastMessage.SenderId = senderId;
            this.lastMessage.SenderName = senderName;
            this.lastMessage.SenderPhotoUrl = senderPhotoUrl;

            return this;
        }

        public ILastMessageBuilder Sent(DateTime dateSent)
        {
            this.lastMessage.DateSent = dateSent;

            return this;
        }

        public ILastMessageBuilder MarkAsRead(bool isRead)
        {
            this.lastMessage.IsRead = isRead;

            return this;
        }

        public LastMessage Build() => this.lastMessage;
    }
}