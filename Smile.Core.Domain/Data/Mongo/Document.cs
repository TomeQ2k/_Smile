using MongoDB.Bson;

namespace Smile.Core.Domain.Data.Mongo
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}