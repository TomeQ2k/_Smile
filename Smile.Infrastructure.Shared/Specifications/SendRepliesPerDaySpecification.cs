using System;
using System.Linq;
using System.Linq.Expressions;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Shared.Specifications
{
    public class SendRepliesPerDaySpecification : Specification<Report>
    {
        private readonly Reply reply;

        private SendRepliesPerDaySpecification(Reply reply)
        {
            this.reply = reply;
        }

        public override Expression<Func<Report, bool>> ToExpression()
            => report => reply.IsAdmin
                        || report.Replies
                            .OrderByDescending(r => r.DateSent)
                            .TakeWhile(r => !r.IsAdmin && r.DateSent.AddDays(1) >= DateTime.Now).Count() < Constants.MaxRepliesPerDay;

        public static SendRepliesPerDaySpecification Create(Reply reply)
            => new SendRepliesPerDaySpecification(reply);
    }
}