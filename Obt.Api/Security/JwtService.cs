using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Obt.Api.Identity;

namespace Obt.Api.Security;

public class JwtService
{
    private readonly JwtOptions _options;
    private readonly SymmetricSecurityKey _key;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _key = SigningKey(_options.Secret);
    }

    public static SymmetricSecurityKey SigningKey(string secret)
    {
        var bytes = Encoding.UTF8.GetBytes(secret);
        if (bytes.Length < 32)
        {
            var padded = new byte[32];
            Array.Copy(bytes, padded, bytes.Length);
            bytes = padded;
        }
        return new SymmetricSecurityKey(bytes);
    }

    public string CreateToken(AuthUserPrincipal user)
    {
        var now = DateTime.UtcNow;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new("id", user.Id),
            new("email", user.Email),
            new("fullName", user.FullName),
            new("role", JsonRole(user.Role))
        };
        if (user.SalesRepCode is not null)
            claims.Add(new Claim("salesRepCode", user.SalesRepCode));
        if (user.CustomerSapId is not null)
            claims.Add(new Claim("customerSapId", user.CustomerSapId));

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: now,
            expires: now.AddHours(_options.TtlHours),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static AuthUserPrincipal FromPrincipal(ClaimsPrincipal principal)
    {
        var id = principal.FindFirstValue("id") ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new InvalidOperationException("Missing user id claim");
        var email = principal.FindFirstValue("email") ?? "";
        var fullName = principal.FindFirstValue("fullName") ?? "";
        var roleRaw = principal.FindFirstValue("role") ?? "sales";
        var role = ParseRole(roleRaw);
        return new AuthUserPrincipal(
            id,
            email,
            fullName,
            role,
            principal.FindFirstValue("salesRepCode"),
            principal.FindFirstValue("customerSapId"));
    }

    private static string JsonRole(UserRole role) => role switch
    {
        UserRole.Customer => "customer",
        UserRole.Sales => "sales",
        UserRole.Scm => "scm",
        UserRole.Admin => "admin",
        _ => role.ToString().ToLowerInvariant()
    };

    private static UserRole ParseRole(string role) => role.ToLowerInvariant() switch
    {
        "customer" => UserRole.Customer,
        "sales" => UserRole.Sales,
        "scm" => UserRole.Scm,
        "admin" => UserRole.Admin,
        _ => throw new InvalidOperationException($"Unknown role {role}")
    };
}
