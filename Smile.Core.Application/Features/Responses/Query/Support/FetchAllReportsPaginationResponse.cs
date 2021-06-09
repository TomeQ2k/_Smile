using System.Collections.Generic;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Support
{
    public class FetchAllReportsPaginationResponse : BaseResponse
    {
        public List<ReportListDto> Reports { get; set; }

        public FetchAllReportsPaginationResponse(Error error = null) : base(error) { }
    }
}