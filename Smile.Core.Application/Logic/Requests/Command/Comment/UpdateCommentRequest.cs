using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Comment;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Comment
{
    public class UpdateCommentRequest : IRequest<UpdateCommentResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.CommentLength)]
        public string Content { get; set; }

        [Required]
        public string CommentId { get; set; }

        [Required]
        public string PostId { get; set; }
    }
}