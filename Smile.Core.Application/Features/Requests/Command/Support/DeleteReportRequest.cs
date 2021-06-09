using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Support;

namespace Smile.Core.Application.Features.Requests.Command.Support
{
    public class DeleteReportRequest : IRequest<DeleteReportResponse>
    {
        [Required]
        public string ReportId { get; set; }
    }
}