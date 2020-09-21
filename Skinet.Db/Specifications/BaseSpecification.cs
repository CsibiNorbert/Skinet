using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(
            Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecification()
        {

        }

        public Expression<Func<T, bool>> Criteria { get; }

        // Initialize it
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
