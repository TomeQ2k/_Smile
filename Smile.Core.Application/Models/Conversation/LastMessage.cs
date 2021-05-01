using System;

namespace Smile.Core.Application.Models.Conversation
{
    public class LastMessage
    {
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }
    }
}