using System.Linq;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> GetFilteredPosts(IPostFiltersParams filters);
    }
}