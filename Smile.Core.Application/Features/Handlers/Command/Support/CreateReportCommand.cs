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
    public class CreateReportCommand : IRequestHandler<CreateReportRequest, CreateReportResponse>
    {
        private readonly IReportManager reportManager;
        private readonly IMapper mapper;

        public CreateReportCommand(IReportManager reportManager, IMapper mapper)
        {
            this.reportManager = reportManager;
            this.mapper = mapper;
        }

        public async Task<CreateReportResponse> Handle(CreateReportRequest request, CancellationToken cancellationToken)
        {
            var createdReport = await reportManager.CreateReport(request.Subject, request.Content, request.Files);

            return createdReport != null ? new CreateReportResponse { Report = mapper.Map<ReportDto>(createdReport) }
                : throw new CrudException("Creating report failed");
        }
    }
}