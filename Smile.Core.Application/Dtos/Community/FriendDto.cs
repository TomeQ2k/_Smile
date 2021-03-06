namespace Smile.Core.Application.Dtos.Community
{
    public class FriendDto
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string SenderName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public bool SenderAccepted { get; set; }
        public bool RecipientAccepted { get; set; }
        public bool IsAccepted { get; set; }
    }
}