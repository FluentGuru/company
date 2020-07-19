using Company.Domain.Entities;
using Company.Domain.Services;
using Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class EfRepository : IRepository
    {
        private readonly CompanyDbContext context;

        public EfRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase
        {
            return context.Set<T>().AnyAsync(predicate);
        }

        public Task CommitAsync<T>(IEnumerable<T> entries) where T : EntityBase
        {
            return context.AddRangeAsync(entries);
        }

        public async Task<IEnumerable<K>> FetchAsync<T, K>(Func<IQueryable<T>, IQueryable<K>> query) where T : EntityBase
        {
            return await query(context.Set<T>()).ToListAsync();
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase
        {
            return context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task MergeAsync<T>(T entry) where T : EntityBase
        {
            return Task.Run(() => context.Update(entry));
        }

        public Task SyncAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
