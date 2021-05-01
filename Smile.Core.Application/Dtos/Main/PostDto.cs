using System;
using System.Collections.Generic;

namespace Smile.Core.Application.Dtos.Main
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string PhotoUrl { get; set; }
        public string AuthorId { get; set; }
        public string GroupId { get; set; }
        public string AuthorName { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }

        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<LikeDto> Likes { get; set; }
    }
}