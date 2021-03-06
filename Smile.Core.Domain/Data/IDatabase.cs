using Smile.Core.Domain.Data.Repositories;
using System;
using System.Threading.Tasks;
using Smile.Core.Domain.Entities.Auth;
using Smile.Core.Domain.Entities.Comment;
using Smile.Core.Domain.Entities.Connection;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Domain.Data
{
    public interface IDatabase : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IPostRepository PostRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IFriendRepository FriendRepository { get; }
        IMessageRepository MessageRepository { get; }
        IStoryRepository StoryRepository { get; }
        IReportRepository ReportRepository { get; }
        IGroupRepository GroupRepository { get; }
        IRepository<GroupMember> GroupMemberRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IRepository<Connection> ConnectionRepository { get; }
        IRepository<Token> TokenRepository { get; }
        IFileRepository FileRepository { get; }

        Task<bool> Complete();

        bool HasChanges();
    }
}