using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Features.Requests.Query.Support;
using Smile.Core.Application.Features.Responses.Query.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Query.Support
{
    public class FetchUserReportsQuery : IRequestHandler<FetchUserReportsPaginationRequest, FetchUserReportsPaginationResponse>
    {
        private readonly ISupportService supportService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public FetchUserReportsQuery(ISupportService supportService, IMapper mapper, IHttpContextService httpContextService)
        {
            this.supportService = supportService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<FetchUserReportsPaginationResponse> Handle(FetchUserReportsPaginationRequest request,
            CancellationToken cancellationToken)
        {
            request.UserId = httpContextService.CurrentUserId;

            var reports = await supportService.FetchReports(request);

            httpContextService.AddPagination(reports.CurrentPage, reports.PageSize, reports.TotalCount,
                reports.TotalPages);

            return new FetchUserReportsPaginationResponse { Reports = mapper.Map<List<ReportListDto>>(reports) };
        }
    }
}