using System;

namespace Smile.Core.Application.Dtos.Main
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string PostId { get; set; }
    }
}