using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Features.Responses.Query.User;

namespace Smile.Core.Application.Features.Requests.Query.User
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        [Required]
        public string UserId { get; set; }
    }
}