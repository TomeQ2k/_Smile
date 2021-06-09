using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class ChangePasswordRequest : IRequest<ChangePasswordResponse>
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(maximumLength: Constants.MaxPasswordLength, MinimumLength = Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }
    }
}