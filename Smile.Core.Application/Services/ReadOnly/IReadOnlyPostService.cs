using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Post;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyPostService
    {
        Task<Post> GetPost(string postId);
        Task<PagedList<Post>> GetPosts(GetPostsPaginationRequest paginationRequest);
    }
}