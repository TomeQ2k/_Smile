using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Profile;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class ChangeEmailRequest : IRequest<ChangeEmailResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string NewEmail { get; set; }
    }
}