using System.ComponentModel.DataAnnotations;

namespace Obt.Api.Identity.Dto;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);
