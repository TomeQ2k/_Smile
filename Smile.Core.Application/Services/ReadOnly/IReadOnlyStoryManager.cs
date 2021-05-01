using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Models.Story;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyStoryManager
    {
        Task<IEnumerable<Story>> FetchStories();

        IEnumerable<StoryWrapper> CreateStoryWrappers(string currentUserId, List<StoryDto> stories);
    }
}