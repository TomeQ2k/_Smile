using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Auth;
using Smile.Core.Application.Features.Responses.Command.Auth;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Features.Handlers.Command.Auth
{
    public class ResetPasswordCommand : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
    {
        private readonly IResetPasswordManager resetPasswordManager;

        public ResetPasswordCommand(IResetPasswordManager resetPasswordManager)
        {
            this.resetPasswordManager = resetPasswordManager;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
            => await resetPasswordManager.ResetPassword(request.UserId, request.Token, request.NewPassword)
            ? new ResetPasswordResponse()
            : throw new ResetPasswordException("Error occurred during setting new password");
    }
}