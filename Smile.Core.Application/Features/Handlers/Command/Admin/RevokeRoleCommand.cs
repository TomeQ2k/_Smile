using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Admin;
using Smile.Core.Application.Features.Responses.Command.Admin;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Features.Handlers.Command.Admin
{
    public class RevokeRoleCommand : IRequestHandler<RevokeRoleRequest, RevokeRoleResponse>
    {
        private readonly IAdminService adminService;
        private readonly INotifier notifier;
        private readonly IHubManager hubManager;

        public RevokeRoleCommand(IAdminService adminService, INotifier notifier, IHubManager hubManager)
        {
            this.adminService = adminService;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<RevokeRoleResponse> Handle(RevokeRoleRequest request, CancellationToken cancellationToken)
        {
            if (await adminService.RevokeRole(request.UserId, request.Role))
            {
                await notifier.Push(NotificationMessages.AdminRevokedNotification, request.UserId, NotificationType.AdminRevoked);

                await hubManager.Invoke(SignalrActions.REFRESH_USER_DATA, request.UserId);

                return new RevokeRoleResponse();
            }

            throw new AdminActionException("Role has not been revoked");
        }
    }
}