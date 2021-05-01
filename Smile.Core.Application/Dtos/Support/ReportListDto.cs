using System;

namespace Smile.Core.Application.Dtos.Support
{
    public class ReportListDto
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Subject { get; set; }
        public bool IsClosed { get; set; }
        public string ReporterName { get; set; }
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }
    }
}