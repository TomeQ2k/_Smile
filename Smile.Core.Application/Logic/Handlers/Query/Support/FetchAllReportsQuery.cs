using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Logic.Requests.Query.Support;
using Smile.Core.Application.Logic.Responses.Query.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Query.Support
{
    public class
        FetchAllReportsQuery : IRequestHandler<FetchAllReportsPaginationRequest, FetchAllReportsPaginationResponse>
    {
        private readonly ISupportService supportService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public FetchAllReportsQuery(ISupportService supportService, IMapper mapper,
            IHttpContextService httpContextService)
        {
            this.supportService = supportService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<FetchAllReportsPaginationResponse> Handle(FetchAllReportsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            request.CurrentUserId = httpContextService.CurrentUserId;

            var reports = await supportService.FetchAllReports(request);

            httpContextService.AddPagination(reports.CurrentPage, reports.PageSize, reports.TotalCount,
                reports.TotalPages);

            return new FetchAllReportsPaginationResponse {Reports = mapper.Map<List<ReportListDto>>(reports)};
        }
    }
}