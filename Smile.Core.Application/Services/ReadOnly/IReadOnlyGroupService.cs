using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Group;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Application.Results;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyGroupService
    {
        Task<Group> GetGroup(string groupId);
        Task<PagedList<Group>> FetchGroups(FetchGroupsPaginationRequest paginationRequest);

        Task<UserGroupsResult> FetchUserGroups();
    }
}