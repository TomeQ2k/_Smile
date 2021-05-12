using System;
using System.Linq;
using System.Linq.Expressions;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class SendMessageOnlyToFriendsSpecification : Specification<User>
    {
        private readonly string recipientId;

        private SendMessageOnlyToFriendsSpecification(string recipientId)
        {
            this.recipientId = recipientId;
        }

        public override Expression<Func<User, bool>> ToExpression()
            => sender => sender.FriendsSent.Concat(sender.FriendsReceived)
                            .Any(f => (f.RecipientId == recipientId || f.SenderId == recipientId)
                                    && (f.SenderAccepted && f.RecipientAccepted));

        public static SendMessageOnlyToFriendsSpecification Create(string recipientId)
            => new SendMessageOnlyToFriendsSpecification(recipientId);
    }
}