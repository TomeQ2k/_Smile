using Smile.Core.Application.Dtos.Auth;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Profile
{
    public class RefreshUserDataResponse : BaseResponse
    {
        public string Token { get; set; }
        public UserAuthDto User { get; set; }

        public RefreshUserDataResponse(Error error = null) : base(error) { }
    }
}