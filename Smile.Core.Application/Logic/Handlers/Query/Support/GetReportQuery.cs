using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.Support;
using Smile.Core.Application.Logic.Responses.Query.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Query.Support
{
    public class GetReportQuery : IRequestHandler<GetReportRequest, GetReportResponse>
    {
        private readonly ISupportService supportService;
        private readonly IMapper mapper;
        private readonly IHttpContextReader httpContextReader;

        public GetReportQuery(ISupportService supportService, IMapper mapper, IHttpContextReader httpContextReader)
        {
            this.supportService = supportService;
            this.mapper = mapper;
            this.httpContextReader = httpContextReader;
        }

        public async Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {
            var report = await supportService.GetReport(request.ReportId, httpContextReader.CurrentUserId);

            return report != null
                ? new GetReportResponse {Report = mapper.Map<ReportDto>(report)}
                : throw new EntityNotFoundException("Report not found");
        }
    }
}