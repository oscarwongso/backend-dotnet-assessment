namespace Obt.Api.Identity.Dto;

public record UserDto(
    string Id,
    string Email,
    string FullName,
    UserRole Role,
    string? SalesRepCode,
    string? CustomerSapId);
