using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Post;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IPagedList<Post>> GetFilteredPosts(IPostFiltersParams filters, (int PageNumber, int PageSize) pagination);
    }
}