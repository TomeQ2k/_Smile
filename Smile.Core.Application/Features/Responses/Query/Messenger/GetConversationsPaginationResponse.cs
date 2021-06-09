using System.Collections.Generic;
using Smile.Core.Application.Models.Conversation;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Messenger
{
    public class GetConversationsPaginationResponse : BaseResponse
    {
        public List<Conversation> Conversations { get; set; }

        public GetConversationsPaginationResponse(Error error = null) : base(error) { }
    }
}