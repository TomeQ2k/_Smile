using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Smile.Core.Domain.Mongo.Models;

namespace Smile.Core.Domain.Mongo
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> FindById(string id);
        Task<IEnumerable<TDocument>> GetAll();
        Task<IEnumerable<TDocument>> GetWhere(Expression<Func<TDocument, bool>> predicate);

        Task Insert(TDocument document);
        Task<bool> Update(TDocument document);
        Task<bool> Delete(string id);
    }
}