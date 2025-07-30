using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HerveyPlayersBooking.Data
{
    public class BookingContextFactory : IDesignTimeDbContextFactory<BookingContext>
    {
        public BookingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookingContext>();

            // Direct SQLite connection string for migrations only.
            optionsBuilder.UseSqlite("Data Source=herveyplayers.db");

            return new BookingContext(optionsBuilder.Options);
        }
    }
}
