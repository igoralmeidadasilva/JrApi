namespace JrApi.Domain.Core.Interfaces.Services;

public interface ILinkGeneratorService
{
    Uri CreatePaginationUri(string resourceName, int pageNumber, int pageSize);
}
