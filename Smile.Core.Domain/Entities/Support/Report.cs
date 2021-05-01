using System;
using System.Collections.Generic;
using System.Linq;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Support
{
    public class Report
    {
        public string Id { get; protected set; } = Utils.Id();
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateUpdated { get; protected set; } = DateTime.Now;
        public string Subject { get; protected set; }
        public string Content { get; protected set; }
        public string ReporterId { get; protected set; }
        public bool IsClosed { get; protected set; }
        public string Email { get; protected set; }

        public virtual User Reporter { get; protected set; }

        public virtual ICollection<Reply> Replies { get; protected set; } = new HashSet<Reply>();
        public virtual ICollection<ReportFile> ReportFiles { get; protected set; } = new HashSet<ReportFile>();

        public static Report Create(string subject, string content, string email = null) => new Report
        {
            Subject = subject,
            Content = content,
            Email = email
        };

        public Report SortReplies()
        {
            Replies = Replies.OrderByDescending(r => r.DateSent).ToList();
            return this;
        }

        public void ToggleStatus()
        {
            IsClosed = !IsClosed;
        }

        public void Update()
        {
            DateUpdated = DateTime.Now;
        }
    }
}