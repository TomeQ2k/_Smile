using Microsoft.EntityFrameworkCore;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Comment;
using Smile.Core.Domain.Entities.Community;
using Smile.Core.Domain.Entities.Connection;
using Smile.Core.Domain.Entities.File;
using Smile.Core.Domain.Entities.Group;
using Smile.Core.Domain.Entities.Messenger;
using Smile.Core.Domain.Entities.Notification;
using Smile.Core.Domain.Entities.Post;
using Smile.Core.Domain.Entities.Story;
using Smile.Core.Domain.Entities.Support;
using Smile.Infrastructure.Persistence.Database.Configs;

namespace Smile.Infrastructure.Persistence.Database
{
    public class DataContext : DbContext
    {
        #region tables

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<ReportFile> ReportFiles { get; set; }
        public virtual DbSet<ReplyFile> ReplyFiles { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<GroupInvite> GroupInvites { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }

        #endregion

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfig().Configure(modelBuilder.Entity<User>());
            new RoleConfig().Configure(modelBuilder.Entity<Role>());
            new UserRoleConfig().Configure(modelBuilder.Entity<UserRole>());

            new PostConfig().Configure(modelBuilder.Entity<Post>());
            new LikeConfig().Configure(modelBuilder.Entity<Like>());

            new FriendConfig().Configure(modelBuilder.Entity<Friend>());
            new MessageConfig().Configure(modelBuilder.Entity<Message>());
            new StoryConfig().Configure(modelBuilder.Entity<Story>());
            new UserStoryConfig().Configure(modelBuilder.Entity<UserStory>());

            new ReportConfig().Configure(modelBuilder.Entity<Report>());
            new ReplyConfig().Configure(modelBuilder.Entity<Reply>());

            new GroupConfig().Configure(modelBuilder.Entity<Group>());
            new GroupMemberConfig().Configure(modelBuilder.Entity<GroupMember>());
            new GroupInviteConfig().Configure(modelBuilder.Entity<GroupInvite>());

            new ConnectionConfig().Configure(modelBuilder.Entity<Connection>());
        }
    }
}