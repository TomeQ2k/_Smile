using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Comment;
using Smile.Core.Application.Logic.Responses.Command.Comment;
using Smile.Core.Application.Services;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Command.Comment
{
    public class CreateCommentCommand : IRequestHandler<CreateCommentRequest, CreateCommentResponse>
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly INotifier notifier;
        private readonly IHttpContextReader httpContextReader;

        public CreateCommentCommand(ICommentService commentService, IMapper mapper, INotifier notifier,
            IHttpContextReader httpContextReader)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.notifier = notifier;
            this.httpContextReader = httpContextReader;
        }

        public async Task<CreateCommentResponse> Handle(CreateCommentRequest request,
            CancellationToken cancellationToken)
        {
            var createdComment = await commentService.CreateComment(request.Content, request.PostId) ??
                                 throw new CrudException("Comment has not been created");

            if (httpContextReader.CurrentUserId != createdComment.UserId)
                await notifier.Push(
                    NotificationMessages.NewCommentNotification(createdComment.User.Username,
                        createdComment.Post.Title), createdComment.Post.AuthorId, NotificationType.NewComment);

            return new CreateCommentResponse {Comment = mapper.Map<CommentDto>(createdComment)};
        }
    }
}