using Microsoft.AspNetCore.Http;
using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.Post;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Main;
using Smile.Core.Domain.Data.Models;

namespace Smile.Infrastructure.Shared.Services
{
    public class PostService : IPostService
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IFilesManager filesManager;
        private readonly IHttpContextReader httpContextReader;

        public PostService(IDatabase database, IReadOnlyProfileService profileService, IFilesManager filesManager, IHttpContextReader httpContextReader)
        {
            this.database = database;
            this.profileService = profileService;
            this.filesManager = filesManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<Post> GetPost(string postId)
            => (await database.PostRepository.Find(p => p.Id == postId && (p.GroupId == null ||
                    (p.Group.AdminId == httpContextReader.CurrentUserId || p.Group.GroupMembers.Any(gm => gm.UserId == httpContextReader.CurrentUserId)))))
                ?.SortComments() ?? throw new EntityNotFoundException("Post not found");

        public async Task<IPagedList<Post>> GetPosts(GetPostsPaginationRequest paginationRequest)
            => await database.PostRepository.GetFilteredPosts(paginationRequest,
                (paginationRequest.PageNumber, paginationRequest.PageSize));

        public async Task<Post> CreatePost(Post post, IFormFile photo = null)
        {
            var user = await profileService.GetCurrentUser();

            if (!string.IsNullOrEmpty(post.GroupId)
                && !user.Groups.Concat(user.GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group))
                    .Any(g => g.Id == post.GroupId))
                throw new NoPermissionsException("You are not member of this group");

            user.Posts.Add(post);

            if (!await database.Complete())
                return null;

            if (photo != null)
            {
                var uploadedPhoto = await filesManager.Upload(photo, $"posts/{post.Id}");
                post.SetPhoto(uploadedPhoto?.Path);

                database.FileRepository.AddFile(uploadedPhoto?.Path);

                return await database.Complete() ? post : null;
            }

            return post;
        }

        public async Task<Post> UpdatePost(Post post, IFormFile photo = null, bool changePhoto = false)
        {
            var user = await profileService.GetCurrentUser();

            if (post.AuthorId != user.Id && !user.IsAdmin())
                throw new NoPermissionsException("You are not allowed to update this post");

            database.PostRepository.Update(post);

            if (changePhoto)
            {
                string filesPath = $"files/posts/{post.Id}";
                filesManager.DeleteDirectory(filesPath);

                await database.FileRepository.DeleteFileByPath(filesPath);

                if (photo != null)
                {
                    string filePath = $"posts/{post.Id}";
                    var uploadedPhoto = await filesManager.Upload(photo, filePath);
                    post.SetPhoto(uploadedPhoto?.Path);

                    database.FileRepository.AddFile(uploadedPhoto?.Path);
                }
                else
                    post.SetPhoto(null);
            }

            return await database.Complete() ? post : null;
        }

        public async Task<bool> DeletePost(string postId)
        {
            var user = await profileService.GetCurrentUser();
            var post = user.Posts.FirstOrDefault(p => p.Id == postId) ?? await database.PostRepository.Get(postId)
                ?? throw new EntityNotFoundException("Post not found");

            if (post.AuthorId != user.Id && !user.IsAdmin() && post.Group?.AdminId != user.Id)
                throw new NoPermissionsException("You have no permissions to perform this action");

            database.PostRepository.Delete(post);

            if (!await database.Complete())
                return false;

            if (post.PhotoUrl != null)
            {
                string filesPath = $"files/posts/{post.Id}";
                filesManager.DeleteDirectory(filesPath);

                await database.FileRepository.DeleteFileByPath(filesPath);

                await database.Complete();
            }

            return true;
        }

        public async Task<LikeResult> LikePost(string postId)
        {
            var user = await profileService.GetCurrentUser();
            var post = await GetPost(postId);

            var like = post.Likes.SingleOrDefault(l => l.UserId == user.Id);

            if (like != null)
            {
                post.Likes.Remove(like);

                return await database.Complete() ? new LikeResult() : null;
            }

            var newLike = Like.Create(user.Id, post.Id);

            user.Likes.Add(newLike);
            post.Likes.Add(newLike);

            return await database.Complete() ? new LikeResult(likeCreated: true, like: newLike) : null;
        }
    }
}