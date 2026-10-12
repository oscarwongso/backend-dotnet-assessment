namespace Obt.Api.Identity.Dto;

public record DemoUsersResponse(string PasswordHint, IReadOnlyList<DemoUser> Users);

public record DemoUser(string Email, string FullName, UserRole Role);
