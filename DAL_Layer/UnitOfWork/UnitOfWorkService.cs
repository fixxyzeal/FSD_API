using DAL_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWorkService : IDisposable, IUnitOfWorkService
    {
        private readonly SystemContext entities = null;

        private bool disposed = false;

        public UnitOfWorkService(SystemContext systemContext)
        {
            entities = systemContext;
            // Temp setting
            //entities.Configuration.ProxyCreationEnabled = false;
            //entities.Configuration.LazyLoadingEnabled = false;
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IGenericService<T> Service<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericService<T>;
            }
            IGenericService<T> repo = new GenericService<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public virtual void BeginTransaction()
        {
            entities.Database.BeginTransaction();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await entities.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual void Commit()
        {
            entities.SaveChanges();
            entities.Database.CurrentTransaction.Commit();
        }

        public async virtual Task CommitAsync()
        {
            await entities.SaveChangesAsync().ConfigureAwait(false);
            entities.Database.CurrentTransaction.Commit();
        }

        public virtual void Rollback()
        {
            entities.Database.CurrentTransaction.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}