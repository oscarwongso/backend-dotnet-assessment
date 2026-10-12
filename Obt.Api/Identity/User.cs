namespace Obt.Api.Identity;

public class User
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string FullName { get; set; }
    public required UserRole Role { get; set; }
    public string? SalesRepCode { get; set; }
}
