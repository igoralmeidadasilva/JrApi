using System;
using JrApi.Models;

namespace JrApi.Repository.Interfaces
{
    // Generic Interface for CRUD on DataBase 
    public interface IDbRepository<T>
    {
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectById(int id);
        Task<T> Insert(T item);
        Task<T> Update(T item, int id);
        Task<bool> Delete(int id);
    }
}
