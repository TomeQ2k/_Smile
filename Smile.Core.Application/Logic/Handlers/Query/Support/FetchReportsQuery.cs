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
    public class FetchReportsQuery : IRequestHandler<FetchReportsPaginationRequest, FetchReportsPaginationResponse>
    {
        private readonly ISupportService supportService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public FetchReportsQuery(ISupportService supportService, IMapper mapper, IHttpContextService httpContextService)
        {
            this.supportService = supportService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<FetchReportsPaginationResponse> Handle(FetchReportsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            request.UserId = httpContextService.CurrentUserId;

            var reports = await supportService.FetchReports(request);

            httpContextService.AddPagination(reports.CurrentPage, reports.PageSize, reports.TotalCount,
                reports.TotalPages);

            return new FetchReportsPaginationResponse {Reports = mapper.Map<List<ReportListDto>>(reports)};
        }
    }
}