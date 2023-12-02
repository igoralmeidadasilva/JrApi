using System;

namespace JrApi.Domain.Interfaces.Repositories
{
    public interface IMongoDbRepository<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItemById(int id);
        T Insert(T item);
        Task<T> Update(T itemUpdate);
        Task<bool> Delete(int id);
    }
}
