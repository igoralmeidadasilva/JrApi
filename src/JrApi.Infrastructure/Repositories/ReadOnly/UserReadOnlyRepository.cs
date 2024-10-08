﻿using System.Data;
using Dapper;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using Microsoft.Data.Sqlite;

namespace JrApi.Infrastructure.Repositories.ReadOnly;

public sealed class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(ApplicationContext context) : base(context)
    { }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT 1 FROM {TableName}  WHERE email = @Email";
        using IDbConnection db = new SqliteConnection(ConnectionString);

        return (await db.QuerySingleOrDefaultAsync<bool>(query, new { Email = email }))!;
    }
}