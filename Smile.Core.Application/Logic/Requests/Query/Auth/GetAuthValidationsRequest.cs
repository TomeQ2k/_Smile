using System.ComponentModel.DataAnnotations;
using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Auth;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Requests.Query.Auth
{
    public class GetAuthValidationsRequest : IRequest<GetAuthValidationsResponse>
    {
        [Required]
        public AuthValidationType AuthValidationType { get; set; }

        [Required]
        public string Content { get; set; }
    }
}