using Smile.Core.Application.Dtos.User;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.User
{
    public class GetUserResponse : BaseResponse
    {
        public UserDtoBase User { get; set; }

        public GetUserResponse(Error error = null) : base(error) { }
    }
}