namespace Smile.Core.Application.Dtos.Group
{
    public class GroupListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public string AdminId { get; set; }
        public string ImageUrl { get; set; }
        public int MembersCount { get; set; }
        public bool IsMember { get; set; }
        public bool IsAccepted { get; set; }
        public bool JoinRequested { get; set; }
        public bool IsInvited { get; set; }
        public bool HasCode { get; set; }
    }
}