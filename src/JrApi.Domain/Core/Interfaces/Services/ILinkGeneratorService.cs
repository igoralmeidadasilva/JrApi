using JrApi.Domain.Models;

namespace JrApi.Domain.Core.Interfaces.Services;

public interface ILinkGeneratorService
{
    Uri CreatePaginationUri(string resourceName, int pageNumber, int pageSize);
    Uri CreateUri(string resourceName);
    IEnumerable<Link> CreateLinksCollection(string resourceName, Guid resourceId);
}
