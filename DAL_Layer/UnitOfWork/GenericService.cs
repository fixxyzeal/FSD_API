using DAL_Layer;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly SystemContext _entities;
        private DbSet<T> _objectSet;

        public GenericService(SystemContext entities)
        {
            _entities = entities;
            _objectSet = entities.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }

            return _objectSet.AsNoTracking().AsEnumerable();
        }

        public IQueryable<T> GetQuery()
        {
            return _objectSet.AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return _objectSet.AsNoTracking();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _objectSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public virtual T Get(Expression<Func<T, bool>> match)
        {
            return _objectSet.FirstOrDefault(match);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> match)
        {
            return await _objectSet.FirstOrDefaultAsync(match).ConfigureAwait(false);
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _objectSet.Find(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _objectSet.FindAsync(match).ConfigureAwait(false);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _objectSet.Where(match).AsNoTracking().ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _objectSet.Where(match).AsNoTracking().ToListAsync();
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _objectSet.Where(predicate);

            foreach (var entity in entities)
            {
                _entities.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public void Add(T entity)
        {
            _entities.Entry(entity).State = EntityState.Added;
        }

        public void Modified(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _entities.Entry(entity).State = EntityState.Deleted;
        }

        public int GetCount()
        {
            return _objectSet.Count();
        }

        public void BulkAdd(IList<T> list)
        {
            _entities.BulkInsert(list);
        }

        public async Task BulkAddAsync(IList<T> list)
        {
            await _entities.BulkInsertAsync(list).ConfigureAwait(false);
        }

        public void BulkUpdate(IList<T> list)
        {
            _entities.BulkUpdate(list);
        }

        public async Task BulkUpdateAsync(IList<T> list)
        {
            await _entities.BulkUpdateAsync(list).ConfigureAwait(false);
        }

        public void BulkDelete(IList<T> list)
        {
            _entities.BulkDelete(list);
        }

        public async Task BulkDeleteAsync(IList<T> list)
        {
            await _entities.BulkDeleteAsync(list).ConfigureAwait(false);
        }

        public void BulkRead(IList<T> list)
        {
            _entities.BulkRead(list);
        }

        public async Task BulkReadAsync(IList<T> list)
        {
            await _entities.BulkReadAsync(list);
        }
    }
}