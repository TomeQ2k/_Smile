using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Features.Responses.Command.Post;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Post
{
    public class CreatePostRequest : IRequest<CreatePostResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.TitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.ContentLength)]
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        [MaxFileSizeValidator(Constants.MaxFileSize)]
        public IFormFile Photo { get; set; }

        public string GroupId { get; set; }
    }
}