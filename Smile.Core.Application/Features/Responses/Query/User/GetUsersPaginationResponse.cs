using System.Collections.Generic;
using Smile.Core.Application.Dtos.User;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.User
{
    public class GetUsersPaginationResponse : BaseResponse
    {
        public List<UserDto> Users { get; set; }

        public GetUsersPaginationResponse(Error error = null) : base(error) { }
    }
}