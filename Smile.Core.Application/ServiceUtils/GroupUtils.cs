using System.Collections.Generic;
using System.Linq;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Pagination;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.ServiceUtils
{
    public static class GroupUtils
    {
        public static void SetGroupMemberParams(List<GroupListDto> groupsToReturn, List<Group> groups, string currentUserId)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                groupsToReturn[i].IsMember = GroupUtils.IsMember(groups[i], currentUserId);
                groupsToReturn[i].IsAccepted = GroupUtils.IsAccepted(groups[i], currentUserId);
                groupsToReturn[i].JoinRequested = GroupUtils.IsJoinRequested(groups[i], currentUserId);
                groupsToReturn[i].IsInvited = GroupUtils.IsInvited(groups[i], currentUserId);
            }
        }

        public static void SetGroupMemberParams(List<GroupListDto> groupsToReturn, PagedList<Group> groups, string currentUserId)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                groupsToReturn[i].IsMember = GroupUtils.IsMember(groups[i], currentUserId);
                groupsToReturn[i].IsAccepted = GroupUtils.IsAccepted(groups[i], currentUserId);
                groupsToReturn[i].JoinRequested = GroupUtils.IsJoinRequested(groups[i], currentUserId);
                groupsToReturn[i].IsInvited = GroupUtils.IsInvited(groups[i], currentUserId);
            }
        }

        #region private

        private static bool IsMember(Group group, string currentUserId) => group.AdminId == currentUserId || group.GroupMembers.Any(m => m.UserId == currentUserId);

        private static bool IsAccepted(Group group, string currentUserId) => group.AdminId == currentUserId || group.GroupMembers.Any(m => m.UserId == currentUserId && m.IsAccepted);

        private static bool IsJoinRequested(Group group, string currentUserId) => group.GroupInvites.Any(i => i.UserId == currentUserId && i.IsJoining);

        private static bool IsInvited(Group group, string currentUserId) => group.GroupInvites.Any(i => i.UserId == currentUserId && i.IsInvited);

        #endregion
    }
}