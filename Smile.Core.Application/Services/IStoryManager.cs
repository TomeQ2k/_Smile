using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Core.Application.Services
{
    public interface IStoryManager : IReadOnlyStoryManager
    {
        Task<Story> AddStory(IFormFile photo);
        Task<bool> WatchStory(string storyId, string userId);
        Task<bool> DeleteStory(string storyId);

        Task ClearStories();
    }
}