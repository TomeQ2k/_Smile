using System;
using System.Collections.Generic;
using System.Linq;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Main
{
    public class Post
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateUpdated { get; protected set; } = DateTime.Now;
        public string PhotoUrl { get; protected set; }
        public string AuthorId { get; protected set; }
        public string GroupId { get; protected set; }

        public virtual User Author { get; protected set; }
        public virtual Group.Group Group { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; protected set; } = new HashSet<Like>();

        public void SetPhoto(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }

        public Post SortComments(bool descending = false)
        {
            Comments = descending ? Comments.OrderByDescending(c => c.DateUpdated).ToList() : Comments.OrderBy(c => c.DateUpdated).ToList();
            return this;
        }
    }
}