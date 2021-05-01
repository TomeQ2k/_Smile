using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Profile;
using Smile.Core.Application.Validators;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Logic.Requests.Command.Profile
{
    public class ChangeUsernameRequest : IRequest<ChangeUsernameResponse>
    {
        [Required]
        [StringLength(maximumLength: Constants.MaxUsernameLength, MinimumLength = Constants.MinUsernameLength)]
        [WhitespacesNotAllowedValidator]
        public string NewUsername { get; set; }
    }
}