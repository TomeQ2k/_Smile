using System;
using System.Collections.Generic;
using System.Linq;
using Smile.Core.Common.Helpers;
using Smile.Core.Domain.Entities.Community;
using Smile.Core.Domain.Entities.Group;
using Smile.Core.Domain.Entities.Main;
using Smile.Core.Domain.Entities.Messenger;
using Smile.Core.Domain.Entities.Story;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Domain.Entities.Auth
{
    public class User
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string PasswordSalt { get; protected set; }
        public DateTime DateRegistered { get; protected set; } = DateTime.Now;
        public string PhotoUrl { get; protected set; }
        public bool EmailConfirmed { get; protected set; }
        public bool IsBlocked { get; protected set; }

        public virtual ICollection<Post> Posts { get; protected set; } = new HashSet<Post>();
        public virtual ICollection<Comment> Comments { get; protected set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; protected set; } = new HashSet<Like>();
        public virtual ICollection<Friend> FriendsSent { get; protected set; } = new HashSet<Friend>();
        public virtual ICollection<Friend> FriendsReceived { get; protected set; } = new HashSet<Friend>();
        public virtual ICollection<Message> MessagesSent { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Message> MessagesReceived { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Story.Story> Stories { get; protected set; } = new HashSet<Story.Story>();
        public virtual ICollection<UserStory> UserStories { get; protected set; } = new HashSet<UserStory>();
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();
        public virtual ICollection<Token> Tokens { get; protected set; } = new HashSet<Token>();
        public virtual ICollection<Report> Reports { get; protected set; } = new HashSet<Report>();
        public virtual ICollection<Group.Group> Groups { get; protected set; } = new HashSet<Group.Group>();
        public virtual ICollection<GroupMember> GroupMembers { get; protected set; } = new HashSet<GroupMember>();
        public virtual ICollection<GroupInvite> GroupInvites { get; protected set; } = new HashSet<GroupInvite>();
        public virtual ICollection<Notification.Notification> Notifications { get; protected set; } = new HashSet<Notification.Notification>();
        public virtual ICollection<Connection.Connection> Connections { get; protected set; } = new HashSet<Connection.Connection>();

        public void ConfirmAccount()
        {
            EmailConfirmed = true;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetUsername(string username)
        {
            Username = username;
        }

        public void SetPassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void UpdateProfile(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public void SetAvatar(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }

        public void ToggleBlock()
        {
            IsBlocked = !IsBlocked;
        }

        public void SortPosts()
        {
            Posts = Posts.OrderByDescending(p => p.DateUpdated).ToList();
        }

        public IEnumerable<Post> GetPostsWithoutGroup()
            => Posts.Where(p => p.GroupId == null);

        public IEnumerable<Group.Group> GetUserGroups()
            => Groups.Concat(GroupMembers.Where(m => m.IsAccepted).Select(m => m.Group));

        public bool IsAdmin() => UserRoles.Any(ur => ur.Role.Name == Constants.AdminRole
            || ur.Role.Name == Constants.HeadAdminRole);
    }
}