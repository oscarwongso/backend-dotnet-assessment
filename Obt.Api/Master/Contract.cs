namespace Obt.Api.Master;

public class Contract
{
    public required string ContractRef { get; set; }
    public required string SoldTo { get; set; }
    public required string SalesUserId { get; set; }
    public string? SapRarRef { get; set; }
}
