using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Admin;

namespace Smile.Core.Application.Features.Requests.Command.Admin
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        [Required]
        public string UserId { get; set; }
    }
}