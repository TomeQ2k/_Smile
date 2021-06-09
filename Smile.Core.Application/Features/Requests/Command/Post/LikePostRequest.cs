using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Post;

namespace Smile.Core.Application.Features.Requests.Command.Post
{
    public class LikePostRequest : IRequest<LikePostResponse>
    {
        [Required]
        public string PostId { get; set; }
    }
}