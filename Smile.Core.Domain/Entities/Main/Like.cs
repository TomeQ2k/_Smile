using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Main
{
    public class Like
    {
        public string UserId { get; protected set; }
        public string PostId { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Post Post { get; protected set; }

        public static Like Create(string userId, string postId) => new Like { UserId = userId, PostId = postId };
    }
}