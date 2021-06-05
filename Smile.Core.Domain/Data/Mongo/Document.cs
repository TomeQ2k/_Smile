using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Smile.Core.Domain.Data.Mongo
{
    public abstract class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}