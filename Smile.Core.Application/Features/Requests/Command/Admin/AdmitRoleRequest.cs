using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Command.Admin;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Features.Requests.Command.Admin
{
    public class AdmitRoleRequest : IRequest<AdmitRoleResponse>
    {
        [Required]
        public RoleName Role { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}