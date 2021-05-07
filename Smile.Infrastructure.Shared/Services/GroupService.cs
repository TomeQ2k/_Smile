using Microsoft.AspNetCore.Http;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Builders;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Query.Group;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Group;
using Smile.Core.Domain.Data.Models;

namespace Smile.Infrastructure.Shared.Services
{
    public class GroupService : IGroupService
    {
        private readonly IDatabase database;
        private readonly IFilesManager filesManager;
        private readonly IReadOnlyProfileService profileService;

        public GroupService(IDatabase database, IFilesManager filesManager, IReadOnlyProfileService profileService)
        {
            this.database = database;
            this.filesManager = filesManager;
            this.profileService = profileService;
        }

        public async Task<Group> GetGroup(string groupId)
        {
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            var currentUser = await profileService.GetCurrentUser();
            var currentUserGroups =
                currentUser.Groups.Concat(currentUser.GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group));

            return group.AdminId == currentUser.Id ||
                   group.GroupMembers.Any(m => m.UserId == currentUser.Id && m.IsAccepted) || currentUser.IsAdmin()
                ? group
                : throw new NoPermissionsException("You are not member of this group");
        }

        public async Task<IPagedList<Group>> FetchGroups(FetchGroupsPaginationRequest paginationRequest)
            => await database.GroupRepository.GetFilteredGroups(paginationRequest,
                (paginationRequest.PageNumber, paginationRequest.PageSize));

        public async Task<Group> CreateGroup(string name, string description, bool isPrivate, IFormFile image,
            string joinCode,
            InviteMemberPermission inviteMemberPermission, RemoveMemberPermission removeMemberPermission)
        {
            if (await GroupExists(name))
                throw new DuplicateException("Group already exists");

            var currentUser = await profileService.GetCurrentUser();

            var group = new GroupBuilder()
                .SetName(name)
                .WithDescription(description)
                .IsPrivate(isPrivate)
                .WithJoinCode(joinCode)
                .WithPermissions(inviteMemberPermission, removeMemberPermission)
                .Build();

            if (image != null)
            {
                var uploadedImage = await filesManager.Upload(image, $"groups/{group.Id}");
                group.SetImage(uploadedImage?.Path);

                database.FileRepository.AddFile(uploadedImage?.Path);
            }

            currentUser.Groups.Add(group);

            return await database.Complete() ? group : null;
        }

        public async Task<GroupMember> JoinGroup(string groupId, string joinCode = null)
        {
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");
            var currentUser = await profileService.GetCurrentUser();

            if (group.AdminId == currentUser.Id || group.GroupMembers.Any(m => m.UserId == currentUser.Id))
                throw new DuplicateException("You are member of this group");

            return await CreateMember(group, currentUser.Id, joinCode);
        }

        public async Task<UserGroupsResult> FetchUserGroups()
        {
            var currentUser = await profileService.GetCurrentUser();

            var ownGroups = currentUser.Groups.OrderByDescending(g => g.DateCreated).ToList();
            var myGroups = currentUser.GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group)
                .OrderByDescending(g => g.DateCreated).ToList();

            return new UserGroupsResult(ownGroups, myGroups);
        }

        #region private

        private async Task<GroupMember> CreateMember(Group group, string userId, string joinCode)
        {
            var member = GroupMember.Create(userId, group.Id);

            if (group.IsPrivate && !string.IsNullOrEmpty(group.JoinCode) && group.JoinCode != joinCode)
                throw new NoPermissionsException("Invalid join code");
            else if (!group.IsPrivate ||
                     (group.IsPrivate && !string.IsNullOrEmpty(group.JoinCode) && group.JoinCode == joinCode))
                member.Accept();
            else
            {
                var groupInvite = GroupInvite.Create(userId, group.Id, isInvited: false);
                group.GroupInvites.Add(groupInvite);
            }

            database.GroupMemberRepository.Add(member);

            return await database.Complete() ? member : null;
        }

        private async Task<bool> GroupExists(string groupName) =>
            await database.GroupRepository.Find(g => g.Name.ToLower() == groupName.ToLower()) != null;

        #endregion
    }
}