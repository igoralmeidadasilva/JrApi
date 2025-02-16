using JrApi.Domain.Core.Interfaces.Services;

namespace JrApi.Presentation.Services;

public sealed class LinkGeneratorService : ILinkGeneratorService
{
    private readonly HttpContext _httpContext;
    private readonly LinkGenerator _linkGenerator;
    private readonly string _baseApiUrl;

    public LinkGeneratorService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
    {
        _httpContext = httpContextAccessor.HttpContext
            ?? throw new Exception("Unable to retrieve an HttpContext object.");
        _linkGenerator = linkGenerator;
        string apiVersion = _httpContext.ApiVersioningFeature().RequestedApiVersion!.ToString();
        _baseApiUrl = string.Format("{0}://{1}/api/v{2}", _httpContext.Request.Scheme, _httpContext.Request.Host, apiVersion);
    }

    public Uri CreatePaginationUri(string resourceName, int pageNumber, int pageSize)
    {
        string queryString = $"?{nameof(pageNumber)}={pageNumber}&{nameof(pageSize)}={pageSize}";
        string fullPath = _baseApiUrl + "/" + resourceName.ToLower() + queryString;
        return new(fullPath);
    }
}