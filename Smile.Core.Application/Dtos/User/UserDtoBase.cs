using System;
using System.Collections.Generic;
using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Dtos.Main;

namespace Smile.Core.Application.Dtos.User
{
    public abstract class UserDtoBase
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime DateRegistered { get; set; }
        public string PhotoUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFriend { get; set; }
        public bool IsFriendAccepted { get; set; }
        public bool IsCurrentUserSender { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<PostDto> Posts { get; set; }
        public ICollection<GroupListDto> Groups { get; set; }
    }
}