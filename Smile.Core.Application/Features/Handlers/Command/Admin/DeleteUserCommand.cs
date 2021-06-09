using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Admin;
using Smile.Core.Application.Features.Responses.Command.Admin;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Admin
{
    public class DeleteUserCommand : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IAdminService adminService;

        public DeleteUserCommand(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
            => await adminService.DeleteUser(request.UserId) ? new DeleteUserResponse()
            : throw new AdminActionException("Deleting user failed");
    }
}