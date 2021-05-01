using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Logic.Responses.Command.Post;
using Smile.Core.Application.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Post
{
    public class UpdatePostRequest : IRequest<UpdatePostResponse>
    {
        [Required]
        public string PostId { get; set; }

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

        public bool ChangePhoto { get; set; }
    }
}