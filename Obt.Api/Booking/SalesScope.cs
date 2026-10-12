using Obt.Api.Identity;
using Obt.Api.Security;

namespace Obt.Api.Booking;

public static class SalesScope
{
    public static bool IsSalesScoped(AuthUserPrincipal? user) =>
        user is not null && user.Role == UserRole.Sales;
}
