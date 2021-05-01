using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Profile;

namespace Smile.Core.Application.Logic.Requests.Command.Profile
{
    public class GenerateChangeEmailTokenRequest : IRequest<GenerateChangeEmailTokenResponse>
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}