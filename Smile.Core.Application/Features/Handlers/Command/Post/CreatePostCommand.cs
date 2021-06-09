using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Post;
using Smile.Core.Application.Features.Responses.Command.Post;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Features.Handlers.Command.Post
{
    public class CreatePostCommand : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHttpContextReader httpContextReader;

        public CreatePostCommand(IPostService postService, IMapper mapper, INotifier notifier,
            IHttpContextReader httpContextReader)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.httpContextReader = httpContextReader;
        }

        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var createdPost = await postService.CreatePost(mapper.Map<Domain.Entities.Post.Post>(request), photo: request.Photo) ??
                              throw new CrudException("Post has not been created");

            if (createdPost.Group != null && httpContextReader.CurrentUserId != createdPost.AuthorId)
            {
                var allMembersIds = createdPost.Group.GroupMembers
                    .Where(m => m.IsAccepted && m.UserId != createdPost.AuthorId).Select(m => m.UserId);

                await notifier.Push(
                    NotificationMessages.NewGroupPostNotification(createdPost.Author.Username, createdPost.Group.Name),
                    createdPost.Group.AdminId, NotificationType.NewGroupPost);

                foreach (var memberId in allMembersIds)
                    await notifier.Push(
                        NotificationMessages.NewGroupPostNotification(createdPost.Author.Username,
                            createdPost.Group.Name), memberId, NotificationType.NewGroupPost);
            }

            return new CreatePostResponse {Post = mapper.Map<PostDto>(createdPost)};
        }
    }
}