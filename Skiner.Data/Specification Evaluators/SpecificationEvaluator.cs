using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skiner.Infrastructure.Specification_Evaluators
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Evaluate includes
            // aggregate because we are combining all of our includes
            // So they can be accumulated
            // Current: TEntity
            query = specification.Includes.Aggregate(query,(current, include) => current.Include(include));

            return query;
        }
    }
}
