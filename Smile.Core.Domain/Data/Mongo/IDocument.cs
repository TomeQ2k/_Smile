using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Smile.Core.Domain.Data.Mongo
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }
    }
}