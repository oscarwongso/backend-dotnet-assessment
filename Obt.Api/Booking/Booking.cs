namespace Obt.Api.Booking;

public class Booking
{
    public required string Id { get; set; }
    public required string ContractRef { get; set; }
    public required string CreatedBy { get; set; }
    public required string Status { get; set; }
    public required string PoNumber { get; set; }
    public int TreatmentQty { get; set; }
}
