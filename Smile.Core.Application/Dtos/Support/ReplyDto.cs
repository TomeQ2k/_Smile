using System;
using System.Collections.Generic;

namespace Smile.Core.Application.Dtos.Support
{
    public class ReplyDto
    {
        public string Id { get; set; }
        public DateTime DateSent { get; set; }
        public string Content { get; set; }
        public string ReportId { get; set; }
        public bool IsAdmin { get; set; }
        public string ReporterName { get; set; }

        public ICollection<AttachmentFileDto> ReplyFiles { get; set; }
    }
}