using AutoMapper;
using Smile.Core.Application.Helpers;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Data;
using System.Linq;
using System.Threading.Tasks;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Logic.Requests.Command.GroupManager;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Application.SmartEnums;
using Smile.Core.Domain.Entities.Group;
using Smile.Infrastructure.Shared.Specifications;

namespace Smile.Infrastructure.Shared.Services
{
    public class GroupManager : IGroupManager
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IFilesManager filesManager;
        private readonly IMapper mapper;
        private readonly INotifier notifier;

        public GroupManager(IDatabase database, IReadOnlyProfileService profileService, IFilesManager filesManager,
            IMapper mapper, INotifier notifier)
        {
            this.database = database;
            this.profileService = profileService;
            this.filesManager = filesManager;
            this.mapper = mapper;
            this.notifier = notifier;
        }

        public async Task<InviteMemberResult> InviteMember(string groupId, string userId)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (currentUser.Id == userId)
                throw new NoPermissionsException("You are not allowed to invite yourself to this group");

            if (!InviteMemberPermissionSmartEnum.FromValue((int)group.InviteMemberPermission)
                .ValidatePermission(currentUser.Id, group))
                throw new NoPermissionsException("You are not allowed to invite members in this group");

            if (IsUserGroupMemberSpecification.Create(userId).IsSatisfied(group))
                throw new DuplicateException("This user is currently member of this group");

            var member = GroupMember.Create(userId, groupId);
            var memberInvite = GroupInvite.Create(userId, groupId, isInvited: true);

            database.GroupMemberRepository.Add(member);
            group.GroupInvites.Add(memberInvite);

            if (await database.Complete())
            {
                await notifier.Push(NotificationMessages.GroupInvitedNotification(group.Name), userId,
                    NotificationType.GroupInvited);

                return new InviteMemberResult(member, memberInvite);
            }

            return null;
        }

        public async Task<GroupMember> AcceptMember(string groupId, string userId, bool accept = true)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            return await AcceptMemberInvite(userId, accept, currentUser.Id, group);
        }

        public async Task<bool> KickMember(string groupId, string userId)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (currentUser.Id == userId)
                throw new NoPermissionsException("You are not allowed to kick yourself");

            if (!RemoveMemberPermissionSmartEnum.FromValue((int)group.RemoveMemberPermission)
                .ValidatePermission(currentUser.Id, group))
                throw new NoPermissionsException("You are not allowed to kick members from this group");

            var member = GetMember(userId, group);

            if (!member.IsAccepted)
                throw new NoPermissionsException("This user is not member of this group");

            database.GroupMemberRepository.Delete(member);

            if (await database.Complete())
            {
                await notifier.Push(NotificationMessages.MemberKickedNotification(group.Name), userId,
                    NotificationType.MemberKicked);

                return true;
            }

            return false;
        }

        public async Task<bool> LeaveGroup(string groupId)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (currentUser.Id == group.AdminId)
                throw new NoPermissionsException("You are not allowed to leave your own group");

            var member = GetMember(currentUser.Id, group);

            if (!member.IsAccepted)
                throw new NoPermissionsException("You are not member of this group");

            database.GroupMemberRepository.Delete(member);

            return await database.Complete();
        }

        public async Task<Group> UpdateGroup(string groupId, UpdateGroupRequest request)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (currentUser.Id != group.AdminId)
                throw new NoPermissionsException("You are not allowed to manage this group");

            group = mapper.Map<UpdateGroupRequest, Group>(request, group);
            group.Update();

            if (request.ChangeImage)
            {
                string filesPath = $"files/groups/{group.Id}";
                filesManager.DeleteDirectory(filesPath);

                await database.FileRepository.DeleteFileByPath(filesPath);

                if (request.Image != null)
                {
                    string filePath = $"groups/{group.Id}";
                    var uploadedImage = await filesManager.Upload(request.Image, filePath);
                    group.SetImage(uploadedImage?.Path);

                    database.FileRepository.AddFile(uploadedImage?.Path);
                }
                else
                    group.SetImage(null);
            }

            return await database.Complete() ? group : null;
        }

        public async Task<bool> DeleteGroup(string groupId)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (!DeleteGroupSpecification.Create(currentUser).IsSatisfied(group))
                throw new NoPermissionsException("You are not allowed to delete this group");

            database.GroupRepository.Delete(group);

            string filePath = $"files/groups/{group.Id}";
            filesManager.DeleteDirectory(filePath);

            await database.FileRepository.DeleteFileByPath(filePath);

            return await database.Complete();
        }

        public async Task<bool> SetModerator(string groupId, string userId, bool isModerator = true)
        {
            var currentUser = await profileService.GetCurrentUser();
            var group = await database.GroupRepository.Get(groupId) ??
                        throw new EntityNotFoundException("Group not found");

            if (currentUser.Id != group.AdminId)
                throw new NoPermissionsException("You are not allowed to manage this group");

            var member = GetMember(userId, group);

            if (!member.IsAccepted)
                throw new NoPermissionsException("This user is not member of this group");

            member.SetIsModerator(isModerator);

            if (await database.Complete())
            {
                await notifier.Push(
                    isModerator
                        ? NotificationMessages.GroupModeratorGrantedNotification(group.Name)
                        : NotificationMessages.GroupModeratorRevokedNotification(group.Name),
                    userId,
                    isModerator ? NotificationType.GroupModeratorGranted : NotificationType.GroupModeratorRevoked);

                return true;
            }

            return false;
        }

        public async Task<CanInviteMemberResult> CanInviteMember(string username, string groupId)
        {
            var userToInvite = await database.UserRepository.Find(u => u.Username.ToLower() == username.ToLower()) ??
                               throw new EntityNotFoundException("This user does not exist");

            var userGroups = userToInvite.Groups.Concat(userToInvite.GroupMembers.Select(m => m.Group));

            return new CanInviteMemberResult(!userGroups.Any(g => g.Id == groupId), userToInvite.Id);
        }

        #region private

        private static GroupMember GetMember(string userId, Group group)
            => group.GroupMembers.FirstOrDefault(m => m.UserId == userId) ??
               throw new EntityNotFoundException("Member not found");

        private async Task<GroupMember> AcceptMemberInvite(string userId, bool accept, string currentUserId,
            Group group)
        {
            var member = GetMember(userId, group);

            if (member.IsAccepted)
                throw new DuplicateException("Member is already accepted");

            var memberInvite = group.GroupInvites.FirstOrDefault(i => i.UserId == userId) ??
                               throw new EntityNotFoundException("Invite not found");

            if (CanMemberAcceptOrDenyGroupInviteSpecification.Create(group, currentUserId, userId).IsSatisfied(memberInvite))
                AcceptOrDeny(accept, ref member);
            else
                throw new NoPermissionsException("You are not allowed to accept this member invite");

            group.GroupInvites.Remove(memberInvite);

            await database.Complete();

            await SendNotificationToUser(userId, accept, group.Name, memberInvite.IsInvited);

            return member;
        }

        private void AcceptOrDeny(bool accept, ref GroupMember member)
        {
            if (accept)
                member.Accept();
            else
                database.GroupMemberRepository.Delete(member);
        }

        private async Task SendNotificationToUser(string userId, bool accept, string groupName, bool isInvited)
        {
            if (accept)
            {
                if (isInvited)
                    await notifier.Push(NotificationMessages.GroupJoinedNotification(groupName),
                        NotificationType.GroupJoined);
                else
                    await notifier.Push(NotificationMessages.GroupInviteAcceptedNotification(groupName), userId,
                        NotificationType.GroupInviteAccepted);
            }
            else
            {
                if (!isInvited)
                    await notifier.Push(NotificationMessages.GroupInviteDeniedNotification(groupName), userId,
                        NotificationType.GroupInviteDenied);
            }
        }

        #endregion
    }
}