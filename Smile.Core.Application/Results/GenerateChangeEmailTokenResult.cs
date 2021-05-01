namespace Smile.Core.Application.Results
{
    public class GenerateChangeEmailTokenResult
    {
        public string UserId { get; }
        public string Token { get; }
        public string NewEmail { get; }

        public GenerateChangeEmailTokenResult(string userId, string token, string newEmail)
        {
            UserId = userId;
            Token = token;
            NewEmail = newEmail;
        }
    }
}