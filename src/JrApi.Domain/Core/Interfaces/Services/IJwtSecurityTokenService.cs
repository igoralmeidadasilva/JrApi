using JrApi.Domain.Entities.Users;
using JrApi.Domain.Models;
using System.Security.Claims;

namespace JrApi.Domain.Core.Interfaces.Services;

public interface IJwtSecurityTokenService
{
    public JsonWebToken GenerateToken(IEnumerable<Claim> claims);
    public JsonWebToken GenerateToken(Claim claim);
    public IEnumerable<Claim> GenerateClaims(User user);
}
