using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Post;
using Smile.Core.Application.Features.Responses.Command.Post;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Post
{
    public class DeletePostCommand : IRequestHandler<DeletePostRequest, DeletePostResponse>
    {
        private readonly IPostService postService;

        public DeletePostCommand(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<DeletePostResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
            => await postService.DeletePost(request.PostId) ? new DeletePostResponse()
                : throw new CrudException("Post has not been deleted");
    }
}