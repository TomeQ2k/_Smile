using Smile.Core.Application.Builders.Interfaces;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Builders
{
    public class UserBuilder : IUserBuilder
    {
        private readonly User user = new User();

        public IUserBuilder SetEmail(string email)
        {
            this.user.SetEmail(email);

            return this;
        }

        public IUserBuilder SetUsername(string username)
        {
            this.user.SetUsername(username);

            return this;
        }

        public IUserBuilder SetPassword(string passwordHash, string passwordSalt)
        {
            this.user.SetPassword(passwordHash, passwordSalt);

            return this;
        }

        public User Build() => this.user;
    }
}