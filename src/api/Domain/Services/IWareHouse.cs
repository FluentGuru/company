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
        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class, new();
        Task<IEnumerable<K>> FetchAsync<T, K>(Func<IQueryable<T>, IQueryable<K>> query) where T : class, new();
    }
}
