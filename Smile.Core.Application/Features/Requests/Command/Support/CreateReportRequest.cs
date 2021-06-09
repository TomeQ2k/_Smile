using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Features.Responses.Command.Support;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Support
{
    public class CreateReportRequest : IRequest<CreateReportResponse>
    {
        [Required]
        [StringLength(Constants.TitleLength)]
        public string Subject { get; set; }

        [Required]
        [StringLength(Constants.ContentLength)]
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        [MaxFilesCountValidator(Constants.MaxFilesCount)]
        [MaxFileSizeValidator(Constants.MaxFileSize, isCollection: true)]
        public ICollection<IFormFile> Files { get; set; }
    }
}