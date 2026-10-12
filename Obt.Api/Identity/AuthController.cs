using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obt.Api.Identity.Dto;
using Obt.Api.Security;

namespace Obt.Api.Identity;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public LoginResponse Login([FromBody] LoginRequest request) => _authService.Login(request);

    [Authorize]
    [HttpGet("me")]
    public object Me()
    {
        var user = JwtService.FromPrincipal(User);
        return new { user = _authService.Me(user) };
    }

    [AllowAnonymous]
    [HttpGet("demo-users")]
    public DemoUsersResponse DemoUsers() => _authService.DemoUsers();
}
