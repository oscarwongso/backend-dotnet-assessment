using Obt.Api.Identity;

namespace Obt.Api.Security;

public record AuthUserPrincipal(
    string Id,
    string Email,
    string FullName,
    UserRole Role,
    string? SalesRepCode,
    string? CustomerSapId);
