using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.Support;

namespace Smile.Core.Application.Logic.Requests.Query.Support
{
    public class FetchUserReportsPaginationRequest : ReportFiltersParams, IRequest<FetchUserReportsPaginationResponse>
    {
        public string UserId { get; set; }
    }
}