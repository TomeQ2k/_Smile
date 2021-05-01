using Microsoft.EntityFrameworkCore;
using Smile.Core.Domain.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Group>> GetFilteredGroups(IGroupFiltersParams filters)
        {
            var groups = context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
                groups = groups.Where(g => g.Name.ToLower().Contains(filters.Name.ToLower()));

            if (filters.IsInvited)
                groups = groups.Where(g => g.GroupInvites.Any(i => i.UserId == filters.UserId && i.IsInvited));

            return await groups.ToListAsync();
        }
    }
}