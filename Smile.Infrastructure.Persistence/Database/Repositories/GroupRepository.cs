using Smile.Core.Domain.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Group;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Application.Extensions;

namespace Smile.Infrastructure.Persistence.Database.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Group>> GetFilteredGroups(IGroupFiltersParams filters, (int PageNumber, int PageSize) pagination)
        {
            var groups = context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
                groups = groups.Where(g => g.Name.ToLower().Contains(filters.Name.ToLower()));

            if (filters.IsInvited)
                groups = groups.Where(g => g.GroupInvites.Any(i => i.UserId == filters.UserId && i.IsInvited));

            groups = GroupAccessStatusSmartEnum.FromValue((int)filters.AccessStatus).Filter(groups);
            groups = GroupJoinStatusSmartEnum.FromValue((int)filters.JoinStatus).Filter(groups, filters.UserId);

            groups = GroupSortTypeSmartEnum.FromValue((int)filters.SortType).Sort(groups);

            return await groups.ToPagedList<Group>(pagination.PageNumber, pagination.PageSize);
        }
    }
}