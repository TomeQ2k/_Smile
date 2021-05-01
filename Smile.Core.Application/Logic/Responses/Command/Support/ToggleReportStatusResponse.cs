using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Support
{
    public class ToggleReportStatusResponse : BaseResponse
    {
        public bool IsClosed { get; set; }

        public ToggleReportStatusResponse(Error error = null) : base(error) { }
    }
}