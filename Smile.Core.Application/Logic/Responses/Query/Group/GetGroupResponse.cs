using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Group
{
    public class GetGroupResponse : BaseResponse
    {
        public GroupDto Group { get; set; }

        public GetGroupResponse(Error error = null) : base(error) { }
    }
}