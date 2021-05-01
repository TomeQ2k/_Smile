using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Group
{
    public class CreateGroupResponse : BaseResponse
    {
        public GroupDto Group { get; set; }

        public CreateGroupResponse(Error error = null) : base(error) { }
    }
}