using Smile.Core.Domain.Entities.Main;

namespace Smile.Core.Application.Results
{
    public class LikeResult
    {
        public bool LikeCreated { get; }
        public Like Like { get; }

        public LikeResult(bool likeCreated = false, Like like = null)
        {
            LikeCreated = likeCreated;
            Like = like;
        }
    }
}