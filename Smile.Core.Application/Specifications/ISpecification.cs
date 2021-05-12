using System;
using System.Linq.Expressions;

namespace Smile.Core.Application.Specifications
{
    public interface ISpecification<T> where T : class, new()
    {
        bool IsSatisfied(T model);
        
        Expression<Func<T, bool>> ToExpression();
    }
}