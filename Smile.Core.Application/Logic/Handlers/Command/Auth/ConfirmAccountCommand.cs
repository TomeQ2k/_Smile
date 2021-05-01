using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.Auth;
using Smile.Core.Application.Logic.Responses.Command.Auth;
using Smile.Core.Application.Services;

namespace Smile.Core.Application.Logic.Handlers.Command.Auth
{
    public class ConfirmAccountCommand : IRequestHandler<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IAuthService authService;

        public ConfirmAccountCommand(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request, CancellationToken cancellationToken)
            => await authService.ConfirmAccount(request.UserId, request.Token)
                ? new ConfirmAccountResponse() : throw new AccountNotConfirmedException("Some error occurred during confirming account");
    }
}