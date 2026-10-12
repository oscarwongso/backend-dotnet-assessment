namespace Obt.Api.Booking;

public static class BookingRules
{
    public static bool HasSapRar(string? sapRarRef) =>
        !string.IsNullOrWhiteSpace(sapRarRef) && sapRarRef != "N/A";
}
