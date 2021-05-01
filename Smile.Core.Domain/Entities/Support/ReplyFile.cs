namespace Smile.Core.Domain.Entities.Support
{
    public class ReplyFile : File.BaseFile
    {
        public string ReplyId { get; protected set; }

        public virtual Reply Reply { get; protected set; }
    }
}