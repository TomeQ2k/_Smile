using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Support;

namespace Smile.Core.Application.Logic.Requests.Query.Support
{
    public class GetReportRequest : IRequest<GetReportResponse>
    {
        [Required]
        public string ReportId { get; set; }
    }
}