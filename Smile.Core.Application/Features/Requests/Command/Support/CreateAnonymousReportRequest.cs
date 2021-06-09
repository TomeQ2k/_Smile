using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Support;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Support
{
    public class CreateAnonymousReportRequest : IRequest<CreateAnonymousReportResponse>
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        [StringLength(Constants.ContentLength)]
        public string Content { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}