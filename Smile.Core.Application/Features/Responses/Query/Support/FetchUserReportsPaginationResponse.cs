using System.Collections.Generic;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Support
{
    public class FetchUserReportsPaginationResponse : BaseResponse
    {
        public List<ReportListDto> Reports { get; set; }

        public FetchUserReportsPaginationResponse(Error error = null) : base(error) { }
    }
}