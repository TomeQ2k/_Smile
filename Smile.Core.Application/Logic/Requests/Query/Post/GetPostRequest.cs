using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Post;

namespace Smile.Core.Application.Logic.Requests.Query.Post
{
    public class GetPostRequest : IRequest<GetPostResponse>
    {
        [Required]
        public string PostId { get; set; }
    }
}