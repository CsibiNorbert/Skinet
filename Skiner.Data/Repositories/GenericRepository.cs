using Microsoft.EntityFrameworkCore;
using Skiner.Data.Contexts;
using Skiner.Infrastructure.Specification_Evaluators;
using Skinet.Core.Entities;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiner.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _StoreContext;
        public GenericRepository(StoreContext storeContext)
        {
            _StoreContext = storeContext;
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _StoreContext.Set<T>().FindAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            //The query gets executed once we say toList or FirstOrDefault
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _StoreContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_StoreContext.Set<T>().AsQueryable(), spec);
        }
    }
}
