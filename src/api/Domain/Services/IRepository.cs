using Company.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.Services
{
    public interface IRepository : IWareHouse
    {
        Task CommitAsync<T>(IEnumerable<T> entries) where T : EntityBase;
        Task MergeAsync<T>(T entry) where T : EntityBase;
        Task SyncAsync();
    }

    public static class RespositoryExtensions
    {
        public static Task CommitAsync<T>(this IRepository respository, params T[] entries) where T : EntityBase
            => respository.CommitAsync(entries);

        public static Task MergeAsync<T, K>(this IRepository repository, Expression<Func<T, K>> identifier, params T[] entries) where T : EntityBase
            => repository.MergeAsync(identifier, entries);

    }
}
