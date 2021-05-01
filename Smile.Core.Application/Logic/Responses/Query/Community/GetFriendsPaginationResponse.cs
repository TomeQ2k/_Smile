using System.Collections.Generic;
using Smile.Core.Application.Dtos.Community;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Community
{
    public class GetFriendsPaginationResponse : BaseResponse
    {
        public List<FriendDto> Friends { get; set; }

        public GetFriendsPaginationResponse(Error error = null) : base(error) { }
    }
}