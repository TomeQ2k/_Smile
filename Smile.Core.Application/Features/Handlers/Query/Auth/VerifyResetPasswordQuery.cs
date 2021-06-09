using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Auth;
using Smile.Core.Application.Features.Responses.Query.Auth;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Auth
{
    public class VerifyResetPasswordQuery : IRequestHandler<VerifyResetPasswordRequest, VerifyResetPasswordResponse>
    {
        private readonly IReadOnlyResetPasswordManager resetPasswordManager;

        public VerifyResetPasswordQuery(IReadOnlyResetPasswordManager resetPasswordManager)
        {
            this.resetPasswordManager = resetPasswordManager;
        }

        public async Task<VerifyResetPasswordResponse> Handle(VerifyResetPasswordRequest request, CancellationToken cancellationToken)
            => await resetPasswordManager.VerifyResetPasswordToken(request.UserId, request.Token) ? new VerifyResetPasswordResponse()
                : throw new TokenException("Token is invalid");
    }
}