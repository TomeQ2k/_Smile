using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.Auth;

namespace Smile.Core.Application.Features.Requests.Query.Auth
{
    public class VerifyResetPasswordRequest : IRequest<VerifyResetPasswordResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}