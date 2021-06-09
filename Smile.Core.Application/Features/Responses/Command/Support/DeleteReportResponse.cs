using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Support
{
    public class DeleteReportResponse : BaseResponse
    {
        public DeleteReportResponse(Error error = null) : base(error) { }
    }
}