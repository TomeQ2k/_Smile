using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Auth;
using Smile.Core.Application.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Auth
{
    public class ResetPasswordRequest : IRequest<ResetPasswordResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxPasswordLength, MinimumLength = Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }
    }
}