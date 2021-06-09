using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Post;
using Smile.Core.Application.Features.Responses.Command.Post;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Post
{
    public class LikePostCommand : IRequestHandler<LikePostRequest, LikePostResponse>
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public LikePostCommand(IPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        public async Task<LikePostResponse> Handle(LikePostRequest request, CancellationToken cancellationToken)
        {
            var likeResult = await postService.LikePost(request.PostId);

            var newLike = likeResult?.Like != null ? mapper.Map<LikeDto>(likeResult.Like) : null;

            return likeResult != null ? new LikePostResponse { LikeCreated = likeResult.LikeCreated, Like = newLike }
                : throw new CrudException("Like has not been changed");
        }
    }
}