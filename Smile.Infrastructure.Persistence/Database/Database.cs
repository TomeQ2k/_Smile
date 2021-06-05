using Smile.Core.Domain.Data;
using Smile.Core.Domain.Data.Repositories;
using Smile.Infrastructure.Persistence.Database.Repositories;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Comment;
using Smile.Core.Domain.Entities.Connection;
using Smile.Core.Domain.Entities.Group;

#pragma warning disable 649

namespace Smile.Infrastructure.Persistence.Database
{
    public class Database : IDatabase
    {
        private readonly DataContext context;

        public Database(DataContext context)
        {
            this.context = context;
        }

        #region repositories

        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository ?? new UserRepository(context);

        private IRepository<Role> roleRepository;
        public IRepository<Role> RoleRepository => roleRepository ?? new Repository<Role>(context);

        private IRepository<UserRole> userRoleRepository;
        public IRepository<UserRole> UserRoleRepository => userRoleRepository ?? new Repository<UserRole>(context);

        private IPostRepository postRepository;
        public IPostRepository PostRepository => postRepository ?? new PostRepository(context);

        private IRepository<Comment> commentRepository;
        public IRepository<Comment> CommentRepository => commentRepository ?? new Repository<Comment>(context);

        private IFriendRepository friendRepository;
        public IFriendRepository FriendRepository => friendRepository ?? new FriendRepository(context);

        private IMessageRepository messageRepository;
        public IMessageRepository MessageRepository => messageRepository ?? new MessageRepository(context);

        private IStoryRepository storyRepository;
        public IStoryRepository StoryRepository => storyRepository ?? new StoryRepository(context);

        private IReportRepository reportRepository;
        public IReportRepository ReportRepository => reportRepository ?? new ReportRepository(context);

        private IGroupRepository groupRepository;
        public IGroupRepository GroupRepository => groupRepository ?? new GroupRepository(context);

        private IRepository<GroupMember> groupMemberRepository;

        public IRepository<GroupMember> GroupMemberRepository =>
            groupMemberRepository ?? new Repository<GroupMember>(context);

        private INotificationRepository notificationRepository;

        public INotificationRepository NotificationRepository =>
            notificationRepository ?? new NotificationRepository(context);

        private IRepository<Connection> connectionRepository;

        public IRepository<Connection> ConnectionRepository =>
            connectionRepository ?? new Repository<Connection>(context);

        private IRepository<Token> tokenRepository;
        public IRepository<Token> TokenRepository => tokenRepository ?? new Repository<Token>(context);

        private IFileRepository fileRepository;
        public IFileRepository FileRepository => fileRepository ?? new FileRepository(context);

        #endregion

        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;

        public bool HasChanges() => context.ChangeTracker.HasChanges();

        public void Dispose()
        {
            context.Dispose();
        }
    }
}