using MongoDB.Bson;
using MongoDB.Driver;
using Smile.Core.Domain.Data.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Smile.Core.Application.Attributes;
using Smile.Core.Application.Settings;

namespace Smile.Infrastructure.Persistence.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> collection;

        public MongoRepository(IMongoDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            this.collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public async Task<TDocument> Get(string id)
            => await Task.Run(() =>
             {
                 var objectId = new ObjectId(id);
                 var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

                 return collection.Find(filter).SingleOrDefaultAsync();
             });

        public virtual async Task<IEnumerable<TDocument>> GetAll()
            => await Task.Run(() => collection.AsQueryable().ToEnumerable());

        public virtual async Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> predicate)
            => (await collection.FindAsync(predicate)).ToEnumerable();

        public async Task Insert(TDocument document)
            => await Task.Run(() => collection.InsertOneAsync(document));

        public async Task Delete(string id)
            => await Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                collection.FindOneAndDeleteAsync(filter);
            });

        #region private

        private protected string GetCollectionName(Type documentType)
            => ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;

        #endregion
    }
}