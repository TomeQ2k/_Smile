using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Post> GetFilteredPosts(IPostFiltersParams filters)
        {
            var posts = context.Posts.Where(p => string.IsNullOrEmpty(filters.Title)
                ? true
                : p.Title.ToLower().Contains(filters.Title.ToLower()));

            posts = !string.IsNullOrEmpty(filters.UserId)
                ? posts.Where(p => p.AuthorId == filters.UserId)
                : (string.IsNullOrEmpty(filters.GroupId)
                    ? posts.Where(p => p.GroupId == null)
                    : posts.Where(p => p.GroupId == filters.GroupId));

            posts = PostSortTypeSmartEnum.FromValue((int) filters.SortType).Sort(posts);

            return posts;
        }
    }
}