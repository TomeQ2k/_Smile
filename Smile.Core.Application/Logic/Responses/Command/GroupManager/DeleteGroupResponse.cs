using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.GroupManager
{
    public class DeleteGroupResponse : BaseResponse
    {
        public DeleteGroupResponse(Error error = null) : base(error) { }
    }
}