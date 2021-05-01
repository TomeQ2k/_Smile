namespace Smile.Core.Application.Results
{
    public class CanInviteMemberResult
    {
        public bool CanInvite { get; }
        public string UserId { get; }

        public CanInviteMemberResult(bool canInvite, string userId)
        {
            CanInvite = canInvite;
            UserId = userId;
        }
    }
}