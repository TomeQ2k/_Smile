using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Auth;

namespace Smile.Core.Application.Features.Requests.Command.Auth
{
    public class SendResetPasswordRequest : IRequest<SendResetPasswordResponse>
    {
        [Required]
        public string Email { get; set; }
    }
}