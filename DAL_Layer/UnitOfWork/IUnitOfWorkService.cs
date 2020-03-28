using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWorkService
    {
        void BeginTransaction();

        void Commit();

        Task CommitAsync();

        void Dispose();

        void Rollback();

        Task<int> SaveAsync();

        IGenericService<T> Service<T>() where T : class;
    }
}