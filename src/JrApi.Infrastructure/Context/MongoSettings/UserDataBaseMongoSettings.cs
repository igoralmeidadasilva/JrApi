using System;

namespace JrApi.Infrastructure.Context.Settings
{
    public sealed class UserDataBaseMongoSettings
    {
        public string? ConnectionURI { get; set; } = null;
        public string? DatabaseName { get; set; } = null;
        public string? CollectionName { get; set; } = null;
    }
}
