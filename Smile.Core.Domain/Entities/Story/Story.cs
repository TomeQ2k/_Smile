using System;
using System.Collections.Generic;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Story
{
    public class Story
    {
        public string Id { get; protected set; } = Utils.Id();
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateExpires { get; protected set; } = DateTime.Now.AddDays(1);
        public string StoryUrl { get; protected set; }
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual ICollection<UserStory> UserStories { get; protected set; } = new HashSet<UserStory>();

        public void SetStoryUrl(string storyUrl)
        {
            StoryUrl = storyUrl;
        }
    }
}