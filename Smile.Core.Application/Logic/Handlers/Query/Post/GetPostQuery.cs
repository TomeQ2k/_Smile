using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.Post;
using Smile.Core.Application.Logic.Responses.Query.Post;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.Post
{
    public class GetPostQuery : IRequestHandler<GetPostRequest, GetPostResponse>
    {
        private readonly IReadOnlyPostService postService;
        private readonly IMapper mapper;

        public GetPostQuery(IReadOnlyPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        public async Task<GetPostResponse> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var post = await postService.GetPost(request.PostId);

            return post != null ? new GetPostResponse { Post = mapper.Map<PostDto>(post) }
                : throw new EntityNotFoundException("Post not found");
        }
    }
}