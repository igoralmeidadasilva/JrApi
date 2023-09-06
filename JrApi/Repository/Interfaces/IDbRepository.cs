using System;
using JrApi.Models;

namespace JrApi.Repository.Interfaces
{
    // Generic Interface for CRUD on DataBase 
    public interface IDbRepository<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItemById(int id);
        T Insert(T item);
        Task<T> Update(T itemBody, T itemUpdate);
        Task<bool> Delete(int id);
    }
}
