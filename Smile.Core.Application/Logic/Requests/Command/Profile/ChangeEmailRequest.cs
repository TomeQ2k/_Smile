using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Profile;

namespace Smile.Core.Application.Logic.Requests.Command.Profile
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