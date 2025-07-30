using HerveyPlayersBooking.Data;
using HerveyPlayersBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HerveyPlayersBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        // =======================
        // CREATE BOOKING
        // =======================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.PaymentConfirmed = false;
                _context.Bookings.Add(booking);
                _context.SaveChanges();

                return RedirectToAction("Confirm");
            }

            return View(booking);
        }

        public IActionResult Confirm()
        {
            return View();
        }

        // =======================
        // MANAGE BOOKINGS
        // =======================
        [HttpGet]
        public IActionResult ManageBookings()
        {
            // Redirect to login if not authenticated
            if (HttpContext.Session.GetString("LoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // Search bookings by name
        [HttpGet]
        public IActionResult SearchByName(string name)
        {
            var bookings = _context.Bookings
                .Where(b => b.Name.Contains(name))
                .ToList();

            return Json(bookings);
        }

        // Get single booking by ID
        [HttpGet]
        public IActionResult GetBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            return Json(booking);
        }

        // Update booking date
        [HttpPut]
        public IActionResult UpdateBookingDate([FromBody] UpdateBookingDto dto)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == dto.Id);
            if (booking == null) return NotFound();

            booking.ShowDate = dto.NewDate;
            _context.SaveChanges();

            return Ok();
        }

        // DTO for update
        public class UpdateBookingDto
        {
            public int Id { get; set; }
            public DateTime NewDate { get; set; }
        }

        // =======================
        // E-TICKET PLACEHOLDER
        // =======================
        [HttpGet]
        public IActionResult TicketSent()
        {
            // Future: send actual email with ticket (PDF/QR code)
            return View();
        }
    }
}

