using Smile.Core.Application.Dtos.Support;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Support
{
    public class CreateAnonymousReportResponse : BaseResponse
    {
        public ReportDto Report { get; set; }

        public CreateAnonymousReportResponse(Error error = null) : base(error) { }
    }
}