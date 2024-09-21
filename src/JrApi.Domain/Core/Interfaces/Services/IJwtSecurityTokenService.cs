using JrApi.Domain.Entities.Users;
using JrApi.Domain.Models;
using System.Security.Claims;

namespace JrApi.Domain.Core.Interfaces.Services;

public interface IJwtSecurityTokenService
{
    public JwtToken GenerateToken(IEnumerable<Claim> claims);
    public JwtToken GenerateToken(Claim claim);
    public IEnumerable<Claim> GenerateClaims(User user);
}
