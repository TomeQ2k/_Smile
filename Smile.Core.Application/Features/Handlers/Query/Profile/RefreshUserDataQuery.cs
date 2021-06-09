using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Auth;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Profile;
using Smile.Core.Application.Features.Responses.Query.Profile;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Profile
{
    public class RefreshUserDataQuery : IRequestHandler<RefreshUserDataRequest, RefreshUserDataResponse>
    {
        private readonly IReadOnlyProfileService profileService;
        private readonly IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator;
        private readonly IMapper mapper;

        public RefreshUserDataQuery(IReadOnlyProfileService profileService, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IMapper mapper)
        {
            this.profileService = profileService;
            this.jwtAuthorizationTokenGenerator = jwtAuthorizationTokenGenerator;
            this.mapper = mapper;
        }

        public async Task<RefreshUserDataResponse> Handle(RefreshUserDataRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await profileService.GetCurrentUser();

            var token = await jwtAuthorizationTokenGenerator.GenerateToken(currentUser);

            if (currentUser != null && !string.IsNullOrEmpty(token))
                return new RefreshUserDataResponse
                {
                    Token = token,
                    User = mapper.Map<UserAuthDto>(currentUser)
                };

            throw new AuthException("Some error occurred during signing in");
        }
    }
}