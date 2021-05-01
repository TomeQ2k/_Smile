using System;
using System.Collections.Generic;

namespace Smile.Core.Application.Dtos.Story
{
    public class StoryDto
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpires { get; set; }
        public string StoryUrl { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserPhotoUrl { get; set; }
        public bool IsWatched { get; set; }
        public int WatchedByCount { get; set; }

        public ICollection<UserStoryDto> UserStories { get; set; }
    }
}