namespace HerveyPlayersBooking.Models
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ShowDate { get; set; }
        public int NumberOfSeats { get; set; }
    }
}