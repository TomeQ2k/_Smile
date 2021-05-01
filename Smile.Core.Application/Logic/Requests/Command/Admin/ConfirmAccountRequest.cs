using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Admin;

namespace Smile.Core.Application.Logic.Requests.Command.Admin
{
    public class ConfirmAccountRequest : IRequest<ConfirmAccountResponse>
    {
        [Required]
        public string UserId { get; set; }
    }
}