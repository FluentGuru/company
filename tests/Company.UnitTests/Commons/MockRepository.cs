using Company.Domain.Entities;
using Company.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.UnitTests.Commons
{
    internal class MockRepository : IRepository
    {
        private readonly List<EntityBase> _collection = new List<EntityBase>();
        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase
        {
            return Task.FromResult(GetQuery<T>().Any(predicate));
        }

        public Task CommitAsync<T>(IEnumerable<T> entries) where T : EntityBase
        {
            _collection.AddRange(entries);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<K>> FetchAsync<T, K>(Func<IQueryable<T>, IQueryable<K>> query) where T : EntityBase
        {
            return Task.FromResult(query(GetQuery<T>()).AsEnumerable());
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase
        {
            return Task.FromResult(GetQuery<T>().FirstOrDefault(predicate));
        }

        public Task MergeAsync<T>(T entry) where T : EntityBase
        {
            _collection.RemoveAll(e => e.Id == entry.Id);
            _collection.Add(entry);
            return Task.CompletedTask;
        }

        public Task SyncAsync()
        {
            return Task.CompletedTask;
        }

        private IQueryable<T> GetQuery<T>() where T : EntityBase
        {
            return _collection.OfType<T>().AsQueryable();
        }
    }
}
