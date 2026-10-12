using System.ComponentModel.DataAnnotations;

namespace Obt.Api.Booking.Dto;

public record CreateBookingRequest(
    [Required] string ContractRef,
    [Required] string PoNumber,
    [Range(1, int.MaxValue)] int TreatmentQty);
