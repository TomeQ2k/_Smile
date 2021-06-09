using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Support;
using Smile.Core.Application.Features.Responses.Command.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Support
{
    public class ToggleReportStatusCommand : IRequestHandler<ToggleReportStatusRequest, ToggleReportStatusResponse>
    {
        private readonly IReportBaseManager reportBaseManager;

        public ToggleReportStatusCommand(IReportBaseManager reportBaseManager)
        {
            this.reportBaseManager = reportBaseManager;
        }

        public async Task<ToggleReportStatusResponse> Handle(ToggleReportStatusRequest request, CancellationToken cancellationToken)
        {
            var toggleReportStatusResult = await reportBaseManager.ToggleReportStatus(request.ReportId);

            return toggleReportStatusResult != null ? new ToggleReportStatusResponse { IsClosed = toggleReportStatusResult.IsClosed }
                : throw new AdminActionException("Toggling report status failed");
        }
    }
}