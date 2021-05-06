using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Application.Extensions;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Common.Helpers;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<User>> GetFilteredUsers(IUserFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var users = context.Users.Where(u =>
                    string.IsNullOrEmpty(filters.Username)
                        ? true
                        : u.Username.ToLower().Contains(filters.Username.ToLower()))
                .Where(u => u.Id != filters.UserId);

            users = EmailConfirmedStatusSmartEnum.FromValue((int)filters.EmailConfirmedStatus).Filter(users);
            users = BlockStatusSmartEnum.FromValue((int)filters.BlockStatus).Filter(users);

            if (filters.OnlyAdmin)
                users = users.Where(u => u.UserRoles.Any(ur => ur.Role.Name == Constants.AdminRole || ur.Role.Name == Constants.HeadAdminRole));

            users = UserSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(users);

            return await users.ToPagedList<User>(pagination.PageNumber, pagination.PageSize);
        }
    }
}