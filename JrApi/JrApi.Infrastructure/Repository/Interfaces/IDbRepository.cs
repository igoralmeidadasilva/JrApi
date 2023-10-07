using System;

namespace JrApi.Infrastructure.Repository.Interfaces
{
    // Generic Interface for CRUD on DataBase 
    public interface IDbRepository<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItemById(int id);
        T Insert(T item);
        Task<T> Update(T itemUpdate);
        Task<bool> Delete(int id);
    }
}
