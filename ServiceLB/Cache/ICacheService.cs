using System;
using System.Threading.Tasks;

namespace ServiceLB
{
    public interface ICacheService
    {
        Task<bool> Delete(string key);

        Task<T> Get<T>(string key);

        Task<bool> Set<T>(string key, T value, DateTime? expire);
    }
}