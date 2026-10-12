using Obt.Api.Common;
using Obt.Api.Identity.Dto;
using Obt.Api.Security;
using Obt.Api.Store;

namespace Obt.Api.Identity;

public class AuthService
{
    private readonly InMemoryStore _store;
    private readonly JwtService _jwtService;

    public AuthService(InMemoryStore store, JwtService jwtService)
    {
        _store = store;
        _jwtService = jwtService;
    }

    public LoginResponse Login(LoginRequest request)
    {
        var user = _store.Users.FirstOrDefault(u =>
            string.Equals(u.Email, request.Email.Trim(), StringComparison.OrdinalIgnoreCase))
            ?? throw ApiException.Unauthorized("Invalid email or password");
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw ApiException.Unauthorized("Invalid email or password");

        var principal = ToPrincipal(user);
        return new LoginResponse(_jwtService.CreateToken(principal), ToDto(principal));
    }

    public UserDto Me(AuthUserPrincipal principal) => ToDto(principal);

    public DemoUsersResponse DemoUsers()
    {
        var list = _store.Users
            .Select(u => new DemoUser(u.Email, u.FullName, u.Role))
            .ToList();
        return new DemoUsersResponse(InMemoryStore.DemoPassword, list);
    }

    private static AuthUserPrincipal ToPrincipal(User user) =>
        new(user.Id, user.Email, user.FullName, user.Role, user.SalesRepCode, null);

    private static UserDto ToDto(AuthUserPrincipal p) =>
        new(p.Id, p.Email, p.FullName, p.Role, p.SalesRepCode, p.CustomerSapId);
}
