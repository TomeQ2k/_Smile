using System.Threading.Tasks;
using Smile.Core.Application.Logic.Requests.Query.Community;
using Smile.Core.Domain.Data.Models;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyFriendService
    {
        Task<IPagedList<Friend>> GetFriends(GetFriendsPaginationRequest paginationRequest);

        Task<int> CountFriendInvites();
    }
}