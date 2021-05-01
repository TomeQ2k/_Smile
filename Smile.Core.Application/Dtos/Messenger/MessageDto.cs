using System;

namespace Smile.Core.Application.Dtos.Messenger
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Text { get; set; }
        public DateTime DateSent { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public bool IsRead { get; set; }
    }
}