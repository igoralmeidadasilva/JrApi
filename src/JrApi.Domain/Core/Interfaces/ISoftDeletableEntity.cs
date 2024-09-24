namespace JrApi.Domain.Core.Interfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; }
    public DateTime DeletedOnUtc { get; }
    public void Delete();
}