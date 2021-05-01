namespace Smile.Core.Application.Results
{
    public class ToggleReportStatusResult
    {
        public bool IsClosed { get; }

        public ToggleReportStatusResult(bool isClosed)
        {
            IsClosed = isClosed;
        }
    }
}