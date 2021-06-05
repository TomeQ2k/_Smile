using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Params;
using Smile.Core.Application.Logic.Responses.Query.LogResponses;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Query.LogRequests
{
    public class GetLogsPaginationRequest : LogFiltersParams, IRequest<GetLogsPaginationResponse>
    {
        public GetLogsPaginationRequest()
        {
            PageSize = Constants.LogsPageSize;
        }
    }
}