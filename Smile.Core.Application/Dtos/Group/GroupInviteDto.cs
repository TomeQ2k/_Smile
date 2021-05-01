namespace Smile.Core.Application.Dtos.Group
{
    public class GroupInviteDto
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public bool IsJoining { get; set; }
        public bool IsInvited { get; set; }
    }
}