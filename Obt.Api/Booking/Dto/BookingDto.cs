namespace Obt.Api.Booking.Dto;

public record BookingDto(
    string Id,
    string ContractRef,
    string CreatedBy,
    string Status,
    string PoNumber,
    int TreatmentQty);
