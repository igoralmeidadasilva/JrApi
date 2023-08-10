using System;
using JrApi.Models;

namespace JrApi.Repository.Interfaces
{
    // Generic Interface for CRUD on DataBase 
    public interface IDbRepository<T>
    {
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectById(int id);
        T Insert(T item);
        Task<T> Update(T itemBody, T itemUpdate);
        Task<bool> Delete(int id);
    }
}
