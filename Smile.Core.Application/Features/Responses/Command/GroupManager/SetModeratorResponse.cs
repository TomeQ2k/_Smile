using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.GroupManager
{
    public class SetModeratorResponse : BaseResponse
    {
        public bool IsModerator { get; set; }

        public SetModeratorResponse(Error error = null) : base(error) { }
    }
}