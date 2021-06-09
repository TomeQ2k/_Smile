using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Support;
using Smile.Core.Application.Features.Responses.Command.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Support
{
    public class CreateAnonymousReportCommand : IRequestHandler<CreateAnonymousReportRequest, CreateAnonymousReportResponse>
    {
        private readonly IReportAnonymousManager reportAnonymousManager;
        private readonly IMapper mapper;

        public CreateAnonymousReportCommand(IReportAnonymousManager reportAnonymousManager, IMapper mapper)
        {
            this.reportAnonymousManager = reportAnonymousManager;
            this.mapper = mapper;
        }

        public async Task<CreateAnonymousReportResponse> Handle(CreateAnonymousReportRequest request, CancellationToken cancellationToken)
        {
            var createdReport = await reportAnonymousManager.CreateAnonymousReport(request.Subject, request.Content, request.Email);

            return createdReport != null ? new CreateAnonymousReportResponse { Report = mapper.Map<ReportDto>(createdReport) }
                : throw new CrudException("Creating anonymous report failed");
        }
    }
}