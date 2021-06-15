using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Smile.Core.Domain.Data.Models;

namespace Smile.Core.Application.Models.Pagination
{
    public class MongoPagedList<T> : List<T>, IPagedList<T>
    {
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public MongoPagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            this.AddRange(items);
        }

        public static async Task<MongoPagedList<T>> CreateAsync(IMongoQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new MongoPagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}