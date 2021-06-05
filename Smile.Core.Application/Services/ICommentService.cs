using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Comment;

namespace Smile.Core.Application.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(string content, string postId);
        Task<Comment> UpdateComment(string content, Comment currentComment);
        Task<bool> DeleteComment(string commentId);
    }
}