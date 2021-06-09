using Smile.Core.Application.Dtos.Profile;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Profile
{
    public class GetProfileResponse : BaseResponse
    {
        public UserProfileDto UserProfile { get; set; }

        public GetProfileResponse(Error error = null) : base(error) { }
    }
}