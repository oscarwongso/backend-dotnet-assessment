using Obt.Api.Booking;
using Obt.Api.Identity;
using Obt.Api.Master;

namespace Obt.Api.Store;

/// <summary>
/// In-memory data. No database. Candidate: read/write these lists from services (do not put lists in controllers).
/// </summary>
public class InMemoryStore
{
    public const string SalesOneId = "11111111-1111-1111-1111-111111111111";
    public const string SalesTwoId = "22222222-2222-2222-2222-222222222222";
    public const string ScmId = "33333333-3333-3333-3333-333333333333";
    public const string AdminId = "44444444-4444-4444-4444-444444444444";
    public const string DemoPassword = "Password123!";
    public const string SalesOneEmail = "alex.rivera@obt.demo";
    public const string SalesTwoEmail = "jordan.lee@obt.demo";
    public const string ScmEmail = "scm.ops@obt.demo";
    public const string AdminEmail = "admin@obt.demo";

    public List<User> Users { get; } = [];
    public List<Customer> Customers { get; } = [];
    public List<Contract> Contracts { get; } = [];
    public List<Obt.Api.Booking.Booking> Bookings { get; } = [];

    public InMemoryStore()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(DemoPassword);
        Users.Add(new User
        {
            Id = SalesOneId,
            Email = SalesOneEmail,
            FullName = "Alex Rivera",
            Role = UserRole.Sales,
            SalesRepCode = "SALES-01",
            PasswordHash = hash
        });
        Users.Add(new User
        {
            Id = SalesTwoId,
            Email = SalesTwoEmail,
            FullName = "Jordan Lee",
            Role = UserRole.Sales,
            SalesRepCode = "SALES-02",
            PasswordHash = hash
        });
        Users.Add(new User
        {
            Id = ScmId,
            Email = ScmEmail,
            FullName = "SCM Operations",
            Role = UserRole.Scm,
            PasswordHash = hash
        });
        Users.Add(new User
        {
            Id = AdminId,
            Email = AdminEmail,
            FullName = "System Admin",
            Role = UserRole.Admin,
            PasswordHash = hash
        });

        Customers.Add(new Customer { SapId = "21002100", Name = "Hospital ABC" });
        Customers.Add(new Customer { SapId = "21008800", Name = "Hospital XYZ" });

        Contracts.Add(new Contract
        {
            ContractRef = "1200012345",
            SoldTo = "21002100",
            SalesUserId = SalesOneId,
            SapRarRef = "RAR-ABC-001"
        });
        Contracts.Add(new Contract
        {
            ContractRef = "N/A-XYZ",
            SoldTo = "21008800",
            SalesUserId = SalesTwoId,
            SapRarRef = null
        });
    }
}
