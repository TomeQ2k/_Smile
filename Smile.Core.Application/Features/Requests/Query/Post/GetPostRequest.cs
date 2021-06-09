using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.Post;

namespace Smile.Core.Application.Features.Requests.Query.Post
{
    public class GetPostRequest : IRequest<GetPostResponse>
    {
        [Required]
        public string PostId { get; set; }
    }
}