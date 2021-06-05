using System;
using System.Collections.Generic;
using System.Linq;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Domain.Entities.Group
{
    public class Group
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsPrivate { get; protected set; }
        public string AdminId { get; protected set; }
        public string ImageUrl { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateUpdated { get; protected set; } = DateTime.Now;
        public string JoinCode { get; protected set; }
        public InviteMemberPermission InviteMemberPermission { get; protected set; } = InviteMemberPermission.Admin;
        public RemoveMemberPermission RemoveMemberPermission { get; protected set; } = RemoveMemberPermission.Admin;

        public virtual User Admin { get; protected set; }

        public virtual ICollection<Post.Post> Posts { get; protected set; } = new HashSet<Post.Post>();
        public virtual ICollection<GroupMember> GroupMembers { get; protected set; } = new HashSet<GroupMember>();
        public virtual ICollection<GroupInvite> GroupInvites { get; protected set; } = new HashSet<GroupInvite>();

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetIsPrivate(bool isPrivate)
        {
            IsPrivate = isPrivate;
        }

        public void SetImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void SetJoinCode(string joinCode)
        {
            JoinCode = joinCode;
        }

        public void SetPermissions(InviteMemberPermission inviteMemberPermission, RemoveMemberPermission removeMemberPermission)
        {
            InviteMemberPermission = inviteMemberPermission;
            RemoveMemberPermission = removeMemberPermission;
        }

        public void Update()
        {
            DateUpdated = DateTime.Now;
        }

        public void SortPosts()
        {
            Posts = Posts.OrderByDescending(p => p.DateUpdated).ToList();
        }

        public void SortMembers()
        {
            GroupMembers = GroupMembers.OrderByDescending(m => m.DateJoined).ToList();
        }

        public int GetMembersCount()
            => GroupMembers.Where(m => m.IsAccepted).Count() + 1;

        public IEnumerable<GroupMember> GetMembers()
            => GroupMembers.Where(m => !m.IsModerator);

        public IEnumerable<GroupMember> GetModerators()
            => GroupMembers.Where(m => m.IsModerator);
    }
}