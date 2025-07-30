using System.ComponentModel.DataAnnotations;

namespace HerveyPlayersBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Show date is required.")]
        public DateTime? ShowDate { get; set; }

        [Required(ErrorMessage = "Please select the number of seats.")]
        [Range(1, 100, ErrorMessage = "Seats must be between 1 and 100.")]
        public int? NumberOfSeats { get; set; }

        public bool PaymentConfirmed { get; set; }

        public string? TicketUrl { get; set; }
    }
}
