using System;
using System.Collections.Generic;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Domain.Entities.Support
{
    public class Reply
    {
        public string Id { get; protected set; } = Utils.Id();
        public DateTime DateSent { get; protected set; } = DateTime.Now;
        public string Content { get; protected set; }
        public string ReportId { get; protected set; }
        public bool IsAdmin { get; protected set; }

        public virtual Report Report { get; protected set; }

        public virtual ICollection<ReplyFile> ReplyFiles { get; protected set; } = new HashSet<ReplyFile>();

        public static Reply Create(string content, bool isAdmin, Report report) => new Reply
        {
            Content = content,
            IsAdmin = isAdmin,
            ReportId = report.Id,
            Report = report
        };
    }
}