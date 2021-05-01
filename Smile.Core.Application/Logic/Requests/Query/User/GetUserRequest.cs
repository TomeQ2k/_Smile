using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.User;

namespace Smile.Core.Application.Logic.Requests.Query.User
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        [Required]
        public string UserId { get; set; }
    }
}