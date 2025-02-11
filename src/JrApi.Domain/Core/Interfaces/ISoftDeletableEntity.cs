namespace JrApi.Domain.Core.Interfaces;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
    DateTime DeletedOnUtc { get; }
    void Delete();
}