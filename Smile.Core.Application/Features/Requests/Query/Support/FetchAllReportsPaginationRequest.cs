using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.Support;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Features.Requests.Query.Support
{
    public class FetchAllReportsPaginationRequest : ReportFiltersParams, IRequest<FetchAllReportsPaginationResponse>
    {
        public ReportType ReportType { get; set; } = ReportType.All;
        public string ReporterName { get; set; }

        public string CurrentUserId { get; set; }
    }
}