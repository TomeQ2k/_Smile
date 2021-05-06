using System.Threading.Tasks;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IPagedList<User>> GetFilteredUsers(IUserFiltersParams filters, (int PageNumber, int PageSize) pagination);
    }
}