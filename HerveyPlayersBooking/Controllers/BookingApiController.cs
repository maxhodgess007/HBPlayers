using HerveyPlayersBooking.Data;
using HerveyPlayersBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HerveyPlayersBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public BookingApiController(BookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // =======================
        // GET: api/BookingApi
        // =======================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return _mapper.Map<List<BookingDto>>(bookings);
        }

        // =======================
        // GET: api/BookingApi/5
        // =======================
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return _mapper.Map<BookingDto>(booking);
        }

        // =======================
        // POST: api/BookingApi
        // =======================
        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(BookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<BookingDto>(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, createdDto);
        }

        // =======================
        // PUT: api/BookingApi/5
        // =======================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingDto bookingDto)
        {
            if (id != bookingDto.Id)
                return BadRequest();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            // Map updated properties
            _mapper.Map(bookingDto, booking);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =======================
        // DELETE: api/BookingApi/5
        // =======================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =======================
        // STRIPE PAYMENT PLACEHOLDER
        // =======================
        [HttpPost("start-payment")]
        public IActionResult StartPayment([FromBody] int bookingId)
        {
            // Placeholder for Stripe integration later
            return Ok(new { message = "Stripe payment integration coming soon", bookingId });
        }
    }
}

