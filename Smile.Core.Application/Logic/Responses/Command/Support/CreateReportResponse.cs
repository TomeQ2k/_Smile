using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Support
{
    public class CreateReportResponse : BaseResponse
    {
        public ReportDto Report { get; set; }

        public CreateReportResponse(Error error = null) : base(error) { }
    }
}