using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Features.Requests.Query.LogRequests;
using Smile.Core.Application.Features.Responses.Query.LogResponses;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Services;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.Features.Handlers.Query.LogQueries
{
    public class GetLogsQuery : IRequestHandler<GetLogsPaginationRequest, GetLogsPaginationResponse>
    {
        private readonly IReadOnlyLogManager logManager;
        private readonly IHttpContextWriter httpContextWriter;

        public GetLogsQuery(IReadOnlyLogManager logManager, IHttpContextWriter httpContextWriter)
        {
            this.logManager = logManager;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetLogsPaginationResponse> Handle(GetLogsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            var logs = await logManager.GetLogs(request);

            httpContextWriter.AddPagination(logs.CurrentPage, logs.PageSize, logs.TotalCount, logs.TotalPages);

            var logsToReturn = new List<LogDocument>(logs);

            return new GetLogsPaginationResponse {Logs = logsToReturn};
        }
    }
}