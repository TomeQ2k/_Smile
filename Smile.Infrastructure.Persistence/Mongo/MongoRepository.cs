using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Smile.Core.Application.Settings;
using Smile.Core.Domain.Mongo;
using Smile.Core.Domain.Mongo.Helpers;
using Smile.Core.Domain.Mongo.Models;

namespace Smile.Infrastructure.Persistence.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        protected readonly IMongoCollection<TDocument> collection;

        public MongoRepository(IMongoDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            this.collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public async Task<TDocument> FindById(string id)
            => await collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

        public virtual async Task<IEnumerable<TDocument>> GetAll()
            => await collection
                .Find(x => true)
                .ToListAsync();

        public virtual async Task<IEnumerable<TDocument>> GetWhere(Expression<Func<TDocument, bool>> predicate)
            => await collection
                .Find(predicate)
                .ToListAsync();

        public async Task Insert(TDocument document)
            => await collection.InsertOneAsync(document);

        public async Task<bool> Update(TDocument document)
        {
            var updateResult =
                await collection.ReplaceOneAsync(filter: x => x.Id == document.Id, replacement: document);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await collection.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        #region private

        private protected string GetCollectionName(Type documentType)
            => ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;

        #endregion
    }
}