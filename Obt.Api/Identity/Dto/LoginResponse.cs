namespace Obt.Api.Identity.Dto;

public record LoginResponse(string Token, UserDto User);
