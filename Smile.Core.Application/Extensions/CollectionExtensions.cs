using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Smile.Core.Application.Models.Pagination;

namespace Smile.Core.Application.Extensions
{
    public static class CollectionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
            where T : class, new()
            => PagedList<T>.Create(collection, pageNumber, pageSize);

        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> collection, int pageNumber, int pageSize)
            where T : class, new()
            => await PagedList<T>.CreateAsync(collection, pageNumber, pageSize);
        
        public static async Task<MongoPagedList<T>> ToPagedList<T>(this IMongoQueryable<T> collection, int pageNumber, int pageSize)
            where T : class, new()
            => await MongoPagedList<T>.CreateAsync(collection, pageNumber, pageSize);

        public static IEnumerable<T> SortRandom<T>(this IEnumerable<T> collection) where T : class, new()
            => collection.OrderBy(x => Guid.NewGuid());
    }
}