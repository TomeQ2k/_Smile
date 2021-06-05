using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Comment;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Comment;
using Smile.Core.Application.Logic.Responses.Command.Comment;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Comment
{
    public class UpdateCommentCommand : IRequestHandler<UpdateCommentRequest, UpdateCommentResponse>
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public UpdateCommentCommand(IPostService postService, ICommentService commentService, IMapper mapper)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public async Task<UpdateCommentResponse> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            var post = await postService.GetPost(request.PostId);
            var currentComment = post.Comments.FirstOrDefault(c => c.Id == request.CommentId) ?? throw new EntityNotFoundException("Comment not found");

            var updatedComment = await commentService.UpdateComment(request.Content, currentComment);

            return updatedComment != null ? new UpdateCommentResponse { Comment = mapper.Map<CommentDto>(updatedComment) }
                : throw new CrudException("Comment has not been updated");
        }
    }
}