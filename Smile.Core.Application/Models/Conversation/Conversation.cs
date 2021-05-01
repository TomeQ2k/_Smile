using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Models.Conversation
{
    public class Conversation
    {
        public string Id => Utils.Id();
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }

        public LastMessage LastMessage { get; set; }
    }
}