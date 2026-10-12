using Obt.Api.Booking.Dto;
using Obt.Api.Common;
using Obt.Api.Security;
using Obt.Api.Store;

namespace Obt.Api.Booking;

/// <summary>
/// Candidate task: implement these methods using <see cref="InMemoryStore.Bookings"/>.
/// Follow the same controller → service → store pattern as <see cref="Master.MasterService"/>.
/// </summary>
public class BookingService
{
    private readonly InMemoryStore _store;

    public BookingService(InMemoryStore store)
    {
        _store = store;
    }

    public IReadOnlyList<BookingDto> List(AuthUserPrincipal user)
    {
        throw ApiException.NotImplemented(
            "Implement: list bookings. sales sees only bookings whose contract.salesUserId is self.");
    }

    public BookingDto Create(AuthUserPrincipal user, CreateBookingRequest request)
    {
        throw ApiException.NotImplemented(
            "Implement: create status=draft on store.Bookings. Reject unknown contract. Sales may only use own contract.");
    }

    public BookingDto Get(AuthUserPrincipal user, string id)
    {
        throw ApiException.NotImplemented("Implement: get by id with the same sales isolation as list.");
    }

    public BookingDto Send(AuthUserPrincipal user, string id)
    {
        throw ApiException.NotImplemented("Implement: draft → sent only. 400 if not draft.");
    }
}
