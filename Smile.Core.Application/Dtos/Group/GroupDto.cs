using System;
using System.Collections.Generic;
using Smile.Core.Application.Dtos.Main;
using Smile.Core.Common.Enums.Permissions;

namespace Smile.Core.Application.Dtos.Group
{
    public class GroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string AdminId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string AdminName { get; set; }
        public string AdminPhotoUrl { get; set; }
        public int MembersCount { get; set; }
        public string JoinCode { get; set; }
        public bool IsMember { get; set; }
        public InviteMemberPermission InviteMemberPermission { get; set; }
        public RemoveMemberPermission RemoveMemberPermission { get; set; }

        public ICollection<PostDto> Posts { get; set; }
        public ICollection<GroupMemberDto> Members { get; set; }
        public ICollection<GroupMemberDto> Moderators { get; set; }
    }
}