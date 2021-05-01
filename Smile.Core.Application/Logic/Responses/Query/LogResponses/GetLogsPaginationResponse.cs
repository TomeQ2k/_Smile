using System.Collections.Generic;
using Smile.Core.Application.Models.Error;
using Smile.Core.Application.Models.Mongo;

namespace Smile.Core.Application.Logic.Responses.Query.LogResponses
{
    public class GetLogsPaginationResponse : BaseResponse
    {
        public List<LogDocument> Logs { get; set; }

        public GetLogsPaginationResponse(Error error = null) : base(error)
        {
        }
    }
}