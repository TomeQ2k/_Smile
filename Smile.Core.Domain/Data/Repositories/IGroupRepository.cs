using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> GetFilteredGroups(IGroupFiltersParams filters);
    }
}