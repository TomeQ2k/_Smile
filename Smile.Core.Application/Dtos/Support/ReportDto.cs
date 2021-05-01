using System;
using System.Collections.Generic;

namespace Smile.Core.Application.Dtos.Support
{
    public class ReportDto
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReporterId { get; set; }
        public bool IsClosed { get; set; }
        public string ReporterName { get; set; }
        public string ReporterPhotoUrl { get; set; }
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }

        public ICollection<ReplyDto> Replies { get; set; }
        public ICollection<AttachmentFileDto> ReportFiles { get; set; }
    }
}