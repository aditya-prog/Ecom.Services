using Ecom.Apps.Core.Entities;
using Ecom.Apps.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecom.Apps.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            // If specification criteria is present , then frame the query accordingly
            if(spec.Criteria != null)
            {
                // spec.Criteria is actually the lambda expression required inside where clause
                query = query.Where(spec.Criteria);
            }
            // query inside aggregate method is the initial accumulator value of type IQueryable<TEntity>,
            // and query and current are of same type => IQueryable<TEntity>
            // Return final accumulator value of type IQueryable<TEntity>
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
