using Microsoft.EntityFrameworkCore;
using Smile.Core.Domain.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetFilteredUsers(IUserFiltersParams filters)
        {
            var users = context.Users.Where(u =>
                    string.IsNullOrEmpty(filters.Username)
                        ? true
                        : u.Username.ToLower().Contains(filters.Username.ToLower()))
                .Where(u => u.Id != filters.UserId);

            users = EmailConfirmedStatusSmartEnum.FromValue((int) filters.EmailConfirmedStatus).Filter(users);
            users = BlockStatusSmartEnum.FromValue((int) filters.BlockStatus).Filter(users);

            return await users.ToListAsync();
        }
    }
}