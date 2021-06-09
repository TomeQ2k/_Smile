using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Auth;

namespace Smile.Core.Application.Features.Requests.Command.Auth
{
    public class ConfirmAccountRequest : IRequest<ConfirmAccountResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}