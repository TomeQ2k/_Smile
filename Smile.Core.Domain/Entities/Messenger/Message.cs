using System;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Messenger
{
    public class Message
    {
        public string Id { get; protected set; } = Utils.Id();
        public string SenderId { get; protected set; }
        public string RecipientId { get; protected set; }
        public string Text { get; protected set; }
        public DateTime DateSent { get; protected set; } = DateTime.Now;
        public bool IsRead { get; protected set; }

        public virtual User Sender { get; protected set; }
        public virtual User Recipient { get; protected set; }

        public static Message Create(string text) => new Message { Text = text };

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}