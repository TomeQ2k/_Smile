using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.Support;

namespace Smile.Core.Application.Logic.Requests.Query.Support
{
    public class FetchReportsPaginationRequest : ReportFiltersParams, IRequest<FetchReportsPaginationResponse>
    {
        public string UserId { get; set; }
    }
}