using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Admin;
using Smile.Core.Application.Logic.Responses.Command.Admin;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Command.Admin
{
    public class AdmitRoleCommand : IRequestHandler<AdmitRoleRequest, AdmitRoleResponse>
    {
        private readonly IAdminService adminService;
        private readonly INotifier notifier;
        private readonly IHubManager hubManager;

        public AdmitRoleCommand(IAdminService adminService, INotifier notifier, IHubManager hubManager)
        {
            this.adminService = adminService;
            this.notifier = notifier;
            this.hubManager = hubManager;
        }

        public async Task<AdmitRoleResponse> Handle(AdmitRoleRequest request, CancellationToken cancellationToken)
        {
            if (await adminService.AdmitRole(request.UserId, request.RoleId))
            {
                await notifier.Push(NotificationMessages.AdminGrantedNotification, request.UserId, NotificationType.AdminGranted);

                await hubManager.Invoke(SignalrActions.REFRESH_USER_DATA, request.UserId);

                return new AdmitRoleResponse();
            }

            throw new AdminActionException("Role has not been admitted");
        }
    }
}