using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Comment;

namespace Smile.Core.Application.Features.Requests.Command.Comment
{
    public class DeleteCommentRequest : IRequest<DeleteCommentResponse>
    {
        [Required]
        public string CommentId { get; set; }
    }
}