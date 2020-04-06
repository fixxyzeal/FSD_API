using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public interface IGenericService<T> where T : class
    {
        void Add(T entity);

        void BulkAdd(IList<T> list);

        Task BulkAddAsync(IList<T> list);

        void BulkDelete(IList<T> list);

        Task BulkDeleteAsync(IList<T> list);

        void BulkRead(IList<T> list);

        Task BulkReadAsync(IList<T> list);

        void BulkUpdate(IList<T> list);

        Task BulkUpdateAsync(IList<T> list);

        void Delete(T entity);

        void DeleteWhere(Expression<Func<T, bool>> predicate);

        T Find(Expression<Func<T, bool>> match);

        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        T Get(Expression<Func<T, bool>> match);

        IQueryable<T> GetAll();

        IEnumerable<T> GetAll(Func<T, bool> predicate = null);

        Task<ICollection<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> match);

        int GetCount();

        Task<int> GetCountAsync();

        IQueryable<T> GetQuery();

        IQueryable<T> GetQueryPaging(IQueryable<T> query, int page, int pagesize);

        void Modified(T entity);

        Task<int> UseSql(string sql);
    }
}