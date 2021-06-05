using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Post;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class PostSortTypeSmartEnum : SmartEnum<PostSortTypeSmartEnum>
    {
        protected PostSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly PostSortTypeSmartEnum Newest = new NewestType();
        public static readonly PostSortTypeSmartEnum Oldest = new OldestType();
        public static readonly PostSortTypeSmartEnum LikesDescending = new LikesDescendingType();
        public static readonly PostSortTypeSmartEnum LikesAscending = new LikesAscendingType();

        public abstract IQueryable<Post> Sort(IQueryable<Post> posts);

        private sealed class NewestType : PostSortTypeSmartEnum
        {
            public NewestType() : base(nameof(Newest), (int)PostSortType.Newest) { }

            public override IQueryable<Post> Sort(IQueryable<Post> posts)
                => posts.OrderByDescending(p => p.DateUpdated);
        }

        private sealed class OldestType : PostSortTypeSmartEnum
        {
            public OldestType() : base(nameof(Oldest), (int)PostSortType.Oldest) { }

            public override IQueryable<Post> Sort(IQueryable<Post> posts)
                => posts.OrderBy(p => p.DateUpdated);
        }

        private sealed class LikesDescendingType : PostSortTypeSmartEnum
        {
            public LikesDescendingType() : base(nameof(LikesDescending), (int)PostSortType.LikesDescending) { }

            public override IQueryable<Post> Sort(IQueryable<Post> posts)
                => posts.OrderByDescending(p => p.Likes.Count);
        }

        private sealed class LikesAscendingType : PostSortTypeSmartEnum
        {
            public LikesAscendingType() : base(nameof(LikesAscending), (int)PostSortType.LikesAscending) { }

            public override IQueryable<Post> Sort(IQueryable<Post> posts)
                => posts.OrderBy(p => p.Likes.Count);
        }
    }
}