using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.Results
{
    public class AuthResult
    {
        public string Token { get; }
        public User User { get; }

        public AuthResult(string token, User user)
        {
            Token = token;
            User = user;
        }
    }
}