using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Support;
using Smile.Core.Application.Features.Responses.Command.Support;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Support
{
    public class DeleteReportCommand : IRequestHandler<DeleteReportRequest, DeleteReportResponse>
    {
        private readonly IReportBaseManager reportBaseManager;

        public DeleteReportCommand(IReportBaseManager reportBaseManager)
        {
            this.reportBaseManager = reportBaseManager;
        }

        public async Task<DeleteReportResponse> Handle(DeleteReportRequest request, CancellationToken cancellationToken)
            => await reportBaseManager.DeleteReport(request.ReportId) ? new DeleteReportResponse()
            : throw new AdminActionException("Deleting report failed");
    }
}