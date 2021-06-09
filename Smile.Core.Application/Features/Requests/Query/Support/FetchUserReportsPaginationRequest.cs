using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.Support;

namespace Smile.Core.Application.Features.Requests.Query.Support
{
    public class FetchUserReportsPaginationRequest : ReportFiltersParams, IRequest<FetchUserReportsPaginationResponse>
    {
        public string UserId { get; set; }
    }
}