using Microsoft.EntityFrameworkCore;
using HerveyPlayersBooking.Models;

namespace HerveyPlayersBooking.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        // Empty constructor only needed for EF Core during design time migrations.
        public BookingContext() { }

        public DbSet<Booking> Bookings { get; set; }
    }
}