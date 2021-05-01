using System.Collections.Generic;
using Smile.Core.Application.Dtos.Messenger;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Messenger
{
    public class GetMessagesThreadPaginationResponse : BaseResponse
    {
        public List<MessageDto> Messages { get; set; }
        public RecipientDto Recipient { get; set; }

        public GetMessagesThreadPaginationResponse(Error error = null) : base(error) { }
    }
}