using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Logic.Requests.Command.Admin;
using Smile.Core.Application.Logic.Responses.Command.Admin;
using Smile.Core.Application.Services;
using Smile.Core.Application.SignalR;

namespace Smile.Core.Application.Logic.Handlers.Command.Admin
{
    public class BlockAccountCommand : IRequestHandler<BlockAccountRequest, BlockAccountResponse>
    {
        private readonly IAdminService adminService;
        private readonly IHubManager hubManager;

        public BlockAccountCommand(IAdminService adminService, IHubManager hubManager)
        {
            this.adminService = adminService;
            this.hubManager = hubManager;
        }

        public async Task<BlockAccountResponse> Handle(BlockAccountRequest request, CancellationToken cancellationToken)
        {
            bool isBlocked = await adminService.BlockAccount(request.UserId);

            await hubManager.Invoke(SignalrActions.REFRESH_USER_DATA, request.UserId);

            return new BlockAccountResponse { IsBlocked = isBlocked };
        }
    }
}