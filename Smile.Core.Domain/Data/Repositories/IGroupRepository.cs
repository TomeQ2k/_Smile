using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IPagedList<Group>> GetFilteredGroups(IGroupFiltersParams filters, (int PageNumber, int PageSize) pagination);
    }
}