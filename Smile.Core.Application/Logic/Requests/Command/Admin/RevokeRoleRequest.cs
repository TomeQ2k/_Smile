using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Command.Admin;

namespace Smile.Core.Application.Logic.Requests.Command.Admin
{
    public class RevokeRoleRequest : IRequest<RevokeRoleResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}