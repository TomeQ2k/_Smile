using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Auth;

namespace Smile.Core.Application.Logic.Requests.Command.Auth
{
    public class ConfirmAccountRequest : IRequest<ConfirmAccountResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}