using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Comment;
using Smile.Core.Application.Features.Responses.Command.Comment;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Comment
{
    public class DeleteCommentCommand : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
    {
        private readonly ICommentService commentService;

        public DeleteCommentCommand(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
            => await commentService.DeleteComment(request.CommentId) ? new DeleteCommentResponse()
               : throw new CrudException("Comment has not been deleted");
    }
}