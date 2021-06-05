using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Smile.Core.Domain.Mongo.Models;

namespace Smile.Core.Domain.Mongo
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> Get(string id);
        Task<IEnumerable<TDocument>> GetAll();
        Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> predicate);

        Task Insert(TDocument document);
        Task<bool> Update(TDocument document);
        Task<bool> Delete(string id);
    }
}