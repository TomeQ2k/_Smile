using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Smile.Core.Domain.Data.Mongo
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> Get(string id);
        Task<IEnumerable<TDocument>> GetAll();
        Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> predicate);

        Task Insert(TDocument document);
        Task Delete(string id);
    }
}