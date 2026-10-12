using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obt.Api.Common;

[ApiController]
public class HealthController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("/api/health")]
    public object Health() => new
    {
        ok = true,
        app = "Order Booking Tool .NET",
        partners = new[] { "assessment" },
        runtime = "dotnet"
    };
}
