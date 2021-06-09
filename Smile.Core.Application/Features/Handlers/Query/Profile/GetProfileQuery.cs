using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Smile.Core.Application.Dtos.Profile;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Features.Requests.Query.Profile;
using Smile.Core.Application.Features.Responses.Query.Profile;
using Smile.Core.Application.Services.ReadOnly;

namespace Smile.Core.Application.Features.Handlers.Query.Profile
{
    public class GetProfileQuery : IRequestHandler<GetProfileRequest, GetProfileResponse>
    {
        private readonly IReadOnlyProfileService profileServie;
        private readonly IMapper mapper;

        public GetProfileQuery(IReadOnlyProfileService profileServie, IMapper mapper)
        {
            this.profileServie = profileServie;
            this.mapper = mapper;
        }

        public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            var userProfile = mapper.Map<UserProfileDto>(await profileServie.GetCurrentUser());

            return userProfile != null ? new GetProfileResponse { UserProfile = userProfile }
                : throw new EntityNotFoundException("User profile not found");
        }
    }
}