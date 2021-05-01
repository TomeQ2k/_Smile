using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Post;

namespace Smile.Core.Application.Logic.Requests.Command.Post
{
    public class LikePostRequest : IRequest<LikePostResponse>
    {
        [Required]
        public string PostId { get; set; }
    }
}