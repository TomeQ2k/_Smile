using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Admin;
using Smile.Core.Application.Logic.Responses.Command.Admin;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Admin
{
    public class ConfirmAccountCommand : IRequestHandler<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IAdminService adminService;

        public ConfirmAccountCommand(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request, CancellationToken cancellationToken)
            => await adminService.ConfirmAccount(request.UserId) ? new ConfirmAccountResponse()
            : throw new AdminActionException("Confirming account failed");
    }
}