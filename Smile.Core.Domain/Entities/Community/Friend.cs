using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Community
{
    public class Friend
    {
        public string SenderId { get; protected set; }
        public string RecipientId { get; protected set; }
        public bool SenderAccepted { get; protected set; } = true;
        public bool RecipientAccepted { get; protected set; }

        public virtual User Sender { get; protected set; }
        public virtual User Recipient { get; protected set; }

        public static Friend Create(string senderId, string recipientId) => new Friend { SenderId = senderId, RecipientId = recipientId };

        public void Accept()
        {
            RecipientAccepted = true;
        }
    }
}