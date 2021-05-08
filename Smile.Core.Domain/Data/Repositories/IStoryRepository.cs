using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IStoryRepository : IRepository<Story>
    {
        Task<IEnumerable<Story>> GetUserCurrentStories(string userId);
    }
}