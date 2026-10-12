using Obt.Api.Booking;

namespace Obt.Api.Tests;

public class BookingRulesTests
{
    [Fact]
    public void HasSapRar()
    {
        Assert.True(BookingRules.HasSapRar("RAR-ABC-001"));
        Assert.False(BookingRules.HasSapRar(null));
        Assert.False(BookingRules.HasSapRar("N/A"));
    }
}
