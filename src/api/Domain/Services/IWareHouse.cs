using Company.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.Services
{
    public interface IWareHouse
    {
        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase;
        Task<IEnumerable<K>> FetchAsync<T, K>(Func<IQueryable<T>, IQueryable<K>> query) where T : EntityBase;
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : EntityBase;
    }

    public static class WareHouseExtensions
    {
        public static Task<IEnumerable<T>> FetchAsync<T>(this IWareHouse wareHouse, Func<IQueryable<T>, IQueryable<T>> query) where T : EntityBase
            => wareHouse.FetchAsync(query);

        public static Task<IEnumerable<T>> FetchAsync<T>(this IWareHouse wareHouse) where T : EntityBase
            => wareHouse.FetchAsync<T>(query => query);
    }
}
