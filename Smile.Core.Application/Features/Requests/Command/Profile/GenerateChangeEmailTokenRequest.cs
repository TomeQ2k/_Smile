using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Profile;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class GenerateChangeEmailTokenRequest : IRequest<GenerateChangeEmailTokenResponse>
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}