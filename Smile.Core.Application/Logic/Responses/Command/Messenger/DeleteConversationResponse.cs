using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Messenger
{
    public class DeleteConversationResponse : BaseResponse
    {
        public DeleteConversationResponse(Error error = null) : base(error) { }
    }
}