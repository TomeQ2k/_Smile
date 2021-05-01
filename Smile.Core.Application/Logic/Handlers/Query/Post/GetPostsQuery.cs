using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Logic.Requests.Query.Post;
using Smile.Core.Application.Logic.Responses.Query.Post;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Logic.Handlers.Query.Post
{
    public class GetPostsQuery : IRequestHandler<GetPostsPaginationRequest, GetPostsPaginationResponse>
    {
        private readonly IReadOnlyPostService postService;
        private readonly IMapper mapper;
        private readonly IHttpContextWriter httpContextWriter;

        public GetPostsQuery(IReadOnlyPostService postService, IMapper mapper, IHttpContextWriter httpContextWriter)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetPostsPaginationResponse> Handle(GetPostsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            var posts = await postService.GetPosts(request);

            httpContextWriter.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return new GetPostsPaginationResponse {Posts = mapper.Map<List<PostDto>>(posts)};
        }
    }
}