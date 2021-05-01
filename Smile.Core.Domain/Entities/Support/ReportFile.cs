namespace Smile.Core.Domain.Entities.Support
{
    public class ReportFile : File.BaseFile
    {
        public string ReportId { get; protected set; }

        public virtual Report Report { get; protected set; }
    }
}