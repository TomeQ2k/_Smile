using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Command.Auth;
using Smile.Core.Application.Features.Responses.Command.Auth;
using Smile.Core.Application.Helpers;
using Smile.Core.Application.Services;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Handlers.Command.Auth
{
    public class SendResetPasswordCommand : IRequestHandler<SendResetPasswordRequest, SendResetPasswordResponse>
    {
        private readonly IResetPasswordManager resetPasswordManager;
        private readonly IEmailSender emailSender;

        public IConfiguration Configuration { get; }

        public SendResetPasswordCommand(IResetPasswordManager resetPasswordManager, IEmailSender emailSender, IConfiguration configuration)
        {
            this.resetPasswordManager = resetPasswordManager;
            this.emailSender = emailSender;
            Configuration = configuration;
        }

        public async Task<SendResetPasswordResponse> Handle(SendResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var sendResetPasswordResult = await resetPasswordManager.GenerateResetPasswordToken(request.Email) ?? throw new TokenException("Error occurred during generating reset password token", ErrorCodes.CannotGenerateToken);

            return await emailSender.Send(EmailMessages.ResetPasswordEmail(sendResetPasswordResult.Email, sendResetPasswordResult.Username,
                    $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}account/resetPassword?userId={sendResetPasswordResult.UserId}&token={sendResetPasswordResult.Token}"))
            ? new SendResetPasswordResponse()
            : throw new TokenException("Error occurred during generating reset password token", ErrorCodes.CannotGenerateToken);
        }
    }
}