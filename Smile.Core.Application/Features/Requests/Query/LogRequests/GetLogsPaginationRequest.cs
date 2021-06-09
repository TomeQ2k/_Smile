using MediatR;
using Smile.Core.Application.Features.Requests.Query.Params;
using Smile.Core.Application.Features.Responses.Query.LogResponses;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Query.LogRequests
{
    public class GetLogsPaginationRequest : LogFiltersParams, IRequest<GetLogsPaginationResponse>
    {
        public GetLogsPaginationRequest()
        {
            PageSize = Constants.LogsPageSize;
        }
    }
}