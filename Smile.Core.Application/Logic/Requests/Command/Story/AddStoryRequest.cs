using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Logic.Responses.Command.Story;
using Smile.Core.Application.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Story
{
    public class AddStoryRequest : IRequest<AddStoryResponse>
    {
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        [MaxFileSizeValidator(Constants.MaxFileSize)]
        public IFormFile Photo { get; set; }
    }
}