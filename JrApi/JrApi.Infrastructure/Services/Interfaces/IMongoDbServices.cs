using System;

namespace JrApi.Infrastructure.Services.Interfaces
{
    public interface IMongoDbServices<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItemById(int id);
        T Insert(T item);
        Task<T> Update(T itemUpdate);
        Task<bool> Delete(int id);
    }
}
