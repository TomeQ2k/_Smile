using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class SetAvatarRequest : IRequest<SetAvatarResponse>
    {
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensionsValidator(".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg")]
        [MaxFileSizeValidator(Constants.MaxFileSize)]
        public IFormFile Avatar { get; set; }
    }
}