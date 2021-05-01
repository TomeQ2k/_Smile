using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Infrastructure.Shared.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;

        public CommentService(IDatabase database, IReadOnlyProfileService profileService, IFilesManager filesManager)
        {
            this.database = database;
            this.profileService = profileService;
        }

        public async Task<Comment> CreateComment(string content, string postId)
        {
            var user = await profileService.GetCurrentUser();
            var post = await database.PostRepository.Get(postId) ?? throw new EntityNotFoundException("Post not found");

            var comment = Comment.Create(content);

            user.Comments.Add(comment);
            post.Comments.Add(comment);

            return await database.Complete() ? comment : null;
        }

        public async Task<Comment> UpdateComment(string content, Comment currentComment)
        {
            var user = await profileService.GetCurrentUser();

            if (currentComment.UserId != user.Id && !user.IsAdmin())
                throw new NoPermissionsException("You are not allowed to update this post");

            currentComment.SetContent(content);
            currentComment.Update();

            database.CommentRepository.Update(currentComment);

            return await database.Complete() ? currentComment : null;
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            var user = await profileService.GetCurrentUser();
            var comment = user.Comments.FirstOrDefault(c => c.Id == commentId) ?? await database.CommentRepository.Get(commentId)
                ?? throw new EntityNotFoundException("Comment not found");

            if (comment.UserId != user.Id && !user.IsAdmin())
                throw new NoPermissionsException("You have no permissions to perform this action");

            database.CommentRepository.Delete(comment);

            return await database.Complete();
        }
    }
}