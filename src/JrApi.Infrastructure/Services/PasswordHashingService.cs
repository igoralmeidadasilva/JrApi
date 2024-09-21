using JrApi.Domain.Core;
using JrApi.Domain.Core.Interfaces.Services;

namespace JrApi.Infrastructure.Services;

public sealed class PasswordHashingService : IPasswordHashingService
{

    private const int SALT = 12;
    public string HashPassword(string password)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(password, nameof(password));

        string salt = BCrypt.Net.BCrypt.GenerateSalt(SALT);
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(hashedPassword, nameof(hashedPassword));
        ArgumentValidator.ThrowIfNullOrWhitespace(providedPassword, nameof(providedPassword));

        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
