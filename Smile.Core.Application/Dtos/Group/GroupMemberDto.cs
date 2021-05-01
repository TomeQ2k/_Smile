using System;

namespace Smile.Core.Application.Dtos.Group
{
    public class GroupMemberDto
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsModerator { get; set; }
        public DateTime DateJoined { get; set; }
        public string Username { get; set; }
        public string UserPhotoUrl { get; set; }
        public bool IsJoining { get; set; }
    }
}