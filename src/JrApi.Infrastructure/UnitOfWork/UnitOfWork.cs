using JrApi.Domain.Core.Interfaces;
using JrApi.Infrastructure.Context;

namespace JrApi.Infrastructure.UnitsOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
    }


    public void BeginTransaction() => _context.Database.BeginTransaction();

    public void Commit() => _context.Database.CommitTransaction();

    public void Dispose() => _context?.Dispose();

    public void Rollback() => _context.Database.RollbackTransaction();

    public void SaveChanges() => _context?.SaveChanges();

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

}
