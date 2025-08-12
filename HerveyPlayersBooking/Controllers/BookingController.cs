using System;
using System.Linq;
using HerveyPlayersBooking.Data;
using HerveyPlayersBooking.Models;
using Microsoft.AspNetCore.Http;      // for Session
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;   // for ILogger
using Microsoft.Extensions.Logging.Abstractions;

namespace HerveyPlayersBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingContext _context;
        private readonly ILogger<BookingController> _logger;

        // NEW: 1-arg overload for tests (uses a no-op logger)
        public BookingController(BookingContext context)
            : this(context, NullLogger<BookingController>.Instance) { }

        public BookingController(BookingContext context, ILogger<BookingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // =======================
        // CREATE BOOKING
        // =======================
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("CREATE GET hit");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            _logger.LogInformation("CREATE POST start Name={Name}", booking?.Name);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CREATE POST invalid model");
                return View(booking);
            }

            booking.PaymentConfirmed = false;
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            _logger.LogInformation("CREATE POST success NewId={Id}", booking.Id);
            return RedirectToAction("Confirm");
        }

        public IActionResult Confirm()
        {
            _logger.LogInformation("CONFIRM view served");
            return View();
        }

        // =======================
        // MANAGE BOOKINGS
        // =======================
        [HttpGet]
        public IActionResult ManageBookings()
        {
            _logger.LogInformation("MANAGE BOOKINGS GET hit");

            // Redirect to login if not authenticated
            if (HttpContext.Session.GetString("LoggedIn") != "true")
            {
                _logger.LogWarning("MANAGE BOOKINGS blocked: user not authenticated");
                return RedirectToAction("Login", "Account");
            }

            _logger.LogInformation("MANAGE BOOKINGS allowed");
            return View();
        }

        // Search bookings by name
        [HttpGet]
        public IActionResult SearchByName(string name)
        {
            _logger.LogInformation("SEARCH start NameQuery={Name}", name);

            var bookings = _context.Bookings
                .Where(b => b.Name.Contains(name))
                .ToList();

            _logger.LogInformation("SEARCH success Count={Count}", bookings.Count);
            return Json(bookings);
        }

        // Get single booking by ID
        [HttpGet]
        public IActionResult GetBooking(int id)
        {
            _logger.LogInformation("GET BOOKING start Id={Id}", id);

            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                _logger.LogWarning("GET BOOKING not found Id={Id}", id);
                return NotFound();
            }

            _logger.LogInformation("GET BOOKING success Id={Id}", id);
            return Json(booking);
        }

        // Update booking date
        [HttpPut]
        public IActionResult UpdateBookingDate([FromBody] UpdateBookingDto dto)
        {
            _logger.LogInformation("UPDATE DATE start Id={Id} NewDate={NewDate}", dto?.Id, dto?.NewDate);

            var booking = _context.Bookings.FirstOrDefault(b => b.Id == dto.Id);
            if (booking == null)
            {
                _logger.LogWarning("UPDATE DATE not found Id={Id}", dto?.Id);
                return NotFound();
            }

            booking.ShowDate = dto.NewDate;
            _context.SaveChanges();

            _logger.LogInformation("UPDATE DATE success Id={Id}", dto.Id);
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
            _logger.LogInformation("TICKET SENT view served");
            return View();
        }
    }
}
