namespace Smile.Core.Domain.Entities.Auth
{
    public class UserRole
    {
        public string UserId { get; protected set; }
        public string RoleId { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Role Role { get; protected set; }

        public static UserRole Create(string userId, string roleId) => new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
    }
}