using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiner.Infrastructure.Specification_Evaluators
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {

            var query = EvaluateCriteria(inputQuery, specification);
            // Evaluate includes
            // aggregate because we are combining all of our includes
            // So they can be accumulated
            // Current: TEntity
            query = specification.Includes.Aggregate(query,(current, include) => current.Include(include));

            return query;
        }

        private static IQueryable<TEntity> EvaluateCriteria(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            if (specification.Criteria != null)
            {
                inputQuery = inputQuery.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(specification.OrderByDescending);
            }

            return inputQuery;
        }
    }
}
