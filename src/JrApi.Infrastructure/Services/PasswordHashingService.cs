using JrApi.Domain.Core.Interfaces.Services;
using JrApi.SharedKernel.Guards;

namespace JrApi.Infrastructure.Services;

public sealed class PasswordHashingService : IPasswordHashingService
{
    private const int SALT = 12;
    public string HashPassword(string password)
    {
        Guard.ThrowIfNullOrWhitespace(password, nameof(password));

        string salt = BCrypt.Net.BCrypt.GenerateSalt(SALT);
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        Guard.ThrowIfNullOrWhitespace(hashedPassword, nameof(hashedPassword));
        Guard.ThrowIfNullOrWhitespace(providedPassword, nameof(providedPassword));

        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}