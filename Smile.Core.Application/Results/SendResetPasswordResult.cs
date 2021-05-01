namespace Smile.Core.Application.Results
{
    public class SendResetPasswordResult
    {
        public string Token { get; }
        public string UserId { get; }
        public string Username { get; }
        public string Email { get; }

        public SendResetPasswordResult(string token, string userId, string username, string email)
        {
            Token = token;
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}