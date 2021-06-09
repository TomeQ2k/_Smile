using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.Support;

namespace Smile.Core.Application.Features.Requests.Query.Support
{
    public class GetReportRequest : IRequest<GetReportResponse>
    {
        [Required]
        public string ReportId { get; set; }
    }
}