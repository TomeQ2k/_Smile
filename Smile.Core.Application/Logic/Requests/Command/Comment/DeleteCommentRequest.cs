using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Comment;

namespace Smile.Core.Application.Logic.Requests.Command.Comment
{
    public class DeleteCommentRequest : IRequest<DeleteCommentResponse>
    {
        [Required]
        public string CommentId { get; set; }
    }
}