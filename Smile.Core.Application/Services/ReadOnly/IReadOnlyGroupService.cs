using System.Threading.Tasks;
using Smile.Core.Application.Features.Requests.Query.Group;
using Smile.Core.Application.Results;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyGroupService
    {
        Task<Group> GetGroup(string groupId);
        Task<IPagedList<Group>> FetchGroups(FetchGroupsPaginationRequest paginationRequest);

        Task<UserGroupsResult> FetchUserGroups();
    }
}