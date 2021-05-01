using System.Collections.Generic;
using System.Threading.Tasks;
using Smile.Core.Domain.Data.Repositories.Params;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetFilteredUsers(IUserFiltersParams filters);
    }
}