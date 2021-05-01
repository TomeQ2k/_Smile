using System;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Main
{
    public class Comment
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Content { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateUpdated { get; protected set; } = DateTime.Now;
        public string UserId { get; protected set; }
        public string PostId { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Post Post { get; protected set; }

        public static Comment Create(string content) => new Comment { Content = content };

        public void SetContent(string content)
        {
            Content = content;
        }

        public void Update()
        {
            DateUpdated = DateTime.Now;
        }
    }
}