using System.Threading.Tasks;
using Smile.Core.Application.Results;

namespace Smile.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyGroupManager
    {
        Task<CanInviteMemberResult> CanInviteMember(string username, string groupId);
    }
}