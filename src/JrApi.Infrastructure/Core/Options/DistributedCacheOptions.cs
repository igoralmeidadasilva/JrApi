namespace JrApi.Infrastructure.Core.Options;

public sealed record DistributedCacheOptions
{
    public bool IsCacheActive { get; set;} = true;
    public int ItemsExpirationMinutes { get; set; } = 5;
    public int ItemByIdExpirationMinutes{ get; set; } = 2;
    public string UsersKey { get; set; } = "user-all";
    public string UserByIdKey { get; set; } = "user-{0,0}";

    public static string GetEntityByIdKey(string key, Guid id)
        => string.Format(key, id); 
}
