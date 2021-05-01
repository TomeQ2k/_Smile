using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Auth;

namespace Smile.Core.Application.Logic.Requests.Command.Auth
{
    public class SendResetPasswordRequest : IRequest<SendResetPasswordResponse>
    {
        [Required]
        public string Email { get; set; }
    }
}