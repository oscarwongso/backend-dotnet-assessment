namespace Obt.Api.Security;

public class JwtOptions
{
    public string Secret { get; set; } = "";
    public int TtlHours { get; set; } = 12;
}
