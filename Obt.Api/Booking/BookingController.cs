using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obt.Api.Booking.Dto;
using Obt.Api.Security;

namespace Obt.Api.Booking;

[ApiController]
[Authorize]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IReadOnlyList<BookingDto> List()
    {
        var user = JwtService.FromPrincipal(User);
        return _bookingService.List(user);
    }

    [HttpPost]
    public BookingDto Create([FromBody] CreateBookingRequest body)
    {
        var user = JwtService.FromPrincipal(User);
        return _bookingService.Create(user, body);
    }

    [HttpGet("{id}")]
    public BookingDto Get(string id)
    {
        var user = JwtService.FromPrincipal(User);
        return _bookingService.Get(user, id);
    }

    [HttpPost("{id}/send")]
    public BookingDto Send(string id)
    {
        var user = JwtService.FromPrincipal(User);
        return _bookingService.Send(user, id);
    }
}
