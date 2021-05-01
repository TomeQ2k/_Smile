using System.Collections.Generic;
using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Support
{
    public class FetchReportsPaginationResponse : BaseResponse
    {
        public List<ReportListDto> Reports { get; set; }

        public FetchReportsPaginationResponse(Error error = null) : base(error) { }
    }
}