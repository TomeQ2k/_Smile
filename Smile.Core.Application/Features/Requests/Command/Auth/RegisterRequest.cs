using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Auth;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Auth
{
    public class RegisterRequest : IRequest<RegisterResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxPasswordLength, MinimumLength = Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxUsernameLength, MinimumLength = Constants.MinUsernameLength)]
        [WhitespacesNotAllowedValidator]
        public string Username { get; set; }
    }
}