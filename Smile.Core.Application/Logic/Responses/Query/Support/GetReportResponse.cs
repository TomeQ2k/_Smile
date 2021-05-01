using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Support
{
    public class GetReportResponse : BaseResponse
    {
        public ReportDto Report { get; set; }

        public GetReportResponse(Error error = null) : base(error) { }
    }
}