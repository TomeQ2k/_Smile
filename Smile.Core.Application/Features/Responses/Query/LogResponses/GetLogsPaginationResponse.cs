using System.Collections.Generic;
using Smile.Core.Application.Models.Error;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.Features.Responses.Query.LogResponses
{
    public class GetLogsPaginationResponse : BaseResponse
    {
        public List<LogDocument> Logs { get; set; }

        public GetLogsPaginationResponse(Error error = null) : base(error)
        {
        }
    }
}