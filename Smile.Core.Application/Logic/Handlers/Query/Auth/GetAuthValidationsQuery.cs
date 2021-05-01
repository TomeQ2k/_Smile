using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Smile.Core.Application.Logic.Requests.Query.Auth;
using Smile.Core.Application.Logic.Responses.Query.Auth;
using Smile.Core.Application.Services;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.Logic.Handlers.Query.Auth
{
    public class GetAuthValidationsQuery : IRequestHandler<GetAuthValidationsRequest, GetAuthValidationsResponse>
    {
        private readonly IAuthValidationService authValidationService;

        public GetAuthValidationsQuery(IAuthValidationService authValidationService)
        {
            this.authValidationService = authValidationService;
        }

        public async Task<GetAuthValidationsResponse> Handle(GetAuthValidationsRequest request, CancellationToken cancellationToken)
        {
            bool isAvailable = request.AuthValidationType switch
            {
                AuthValidationType.Email => !await authValidationService.EmailExists(request.Content),
                AuthValidationType.Username => !await authValidationService.UsernameExists(request.Content),
                _ => false
            };

            return new GetAuthValidationsResponse { IsAvailable = isAvailable };
        }
    }
}