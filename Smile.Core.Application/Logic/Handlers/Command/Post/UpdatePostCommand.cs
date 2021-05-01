using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Post;
using Smile.Core.Application.Logic.Responses.Command.Post;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Post
{
    public class UpdatePostCommand : IRequestHandler<UpdatePostRequest, UpdatePostResponse>
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public UpdatePostCommand(IPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        public async Task<UpdatePostResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var post = await postService.GetPost(request.PostId);
            post = mapper.Map<UpdatePostRequest, Domain.Entities.Main.Post>(request, post);

            var updatedPost = await postService.UpdatePost(post, photo: request.Photo, changePhoto: request.ChangePhoto);

            return updatedPost != null ? new UpdatePostResponse { Post = mapper.Map<PostDto>(updatedPost) }
                : throw new CrudException("Post has not been updated");
        }
    }
}