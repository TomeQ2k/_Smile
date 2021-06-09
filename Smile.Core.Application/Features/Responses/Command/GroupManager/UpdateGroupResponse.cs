using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.GroupManager
{
    public class UpdateGroupResponse : BaseResponse
    {
        public GroupDto Group { get; set; }

        public UpdateGroupResponse(Error error = null) : base(error) { }
    }
}