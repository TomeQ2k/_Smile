using System.Collections.Generic;
using System.Linq;
using Smile.Core.Application.Dtos.Story;

namespace Smile.Core.Application.Models.Story
{
    public class StoryWrapper
    {
        public string UserId { get; }
        public string Username { get; }
        public string UserPhotoUrl { get; }
        public bool IsWatched { get; private set; }

        public List<StoryDto> Stories { get; }

        public StoryWrapper(string userId, string username, string userPhotoUrl, List<StoryDto> stories)
        {
            UserId = userId;
            Username = username;
            UserPhotoUrl = userPhotoUrl;

            Stories = new List<StoryDto>(stories);
        }

        public void SetIsWatched()
        {
            IsWatched = Stories.All(s => s.IsWatched);
        }
    }
}