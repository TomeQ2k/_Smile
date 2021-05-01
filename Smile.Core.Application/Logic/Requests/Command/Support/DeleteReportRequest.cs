using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Support;

namespace Smile.Core.Application.Logic.Requests.Command.Support
{
    public class DeleteReportRequest : IRequest<DeleteReportResponse>
    {
        [Required]
        public string ReportId { get; set; }
    }
}