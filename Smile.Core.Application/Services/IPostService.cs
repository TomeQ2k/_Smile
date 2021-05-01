using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Core.Application.Services
{
    public interface IPostService : IReadOnlyPostService
    {
        Task<Post> CreatePost(Post post, IFormFile photo = null);
        Task<Post> UpdatePost(Post post, IFormFile photo = null, bool changePhoto = false);
        Task<bool> DeletePost(string postId);

        Task<LikeResult> LikePost(string postId);
    }
}