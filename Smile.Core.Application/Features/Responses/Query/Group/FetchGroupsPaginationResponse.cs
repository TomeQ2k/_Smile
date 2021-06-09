using System.Collections.Generic;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Group
{
    public class FetchGroupsPaginationResponse : BaseResponse
    {
        public List<GroupListDto> Groups { get; set; }

        public FetchGroupsPaginationResponse(Error error = null) : base(error) { }
    }
}