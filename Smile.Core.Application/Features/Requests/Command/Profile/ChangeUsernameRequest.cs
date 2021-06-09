using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Profile;
using Smile.Core.Application.Validation.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class ChangeUsernameRequest : IRequest<ChangeUsernameResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.MaxUsernameLength, MinimumLength = Constants.MinUsernameLength)]
        [WhitespacesNotAllowedValidator]
        public string NewUsername { get; set; }
    }
}