using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Builders.Interfaces
{
    public interface IUserBuilder : IBuilder<User>
    {
        IUserBuilder SetEmail(string email);
        IUserBuilder SetUsername(string username);
        IUserBuilder SetPassword(string passwordHash, string passwordSalt);
    }
}