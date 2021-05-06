using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Post;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyPostService
    {
        Task<Post> GetPost(string postId);
        Task<IPagedList<Post>> GetPosts(GetPostsPaginationRequest paginationRequest);
    }
}