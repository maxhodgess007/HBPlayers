using Microsoft.EntityFrameworkCore;
using HerveyPlayersBooking.Data;
using HerveyPlayersBooking.Models;
using HerveyPlayersBooking.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;

namespace HerveyPlayersBooking.Tests
{
    public class BookingControllerTests
    {
        // Helper: create fresh in-memory DB context
        private BookingContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB for each test
                .Options;
            return new BookingContext(options);
        }

        // =======================
        // CREATE BOOKING TESTS
        // =======================

        [Fact]
        public void Create_PostValidModel_RedirectsToConfirm()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BookingController(context);
            var booking = new Booking
            {
                Name = "Test",
                Email = "test@test.com",
                ShowDate = DateTime.Now,
                NumberOfSeats = 2
            };

            // Act
            var result = controller.Create(booking);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Confirm", redirectResult.ActionName);
            Assert.Single(context.Bookings);
        }

        [Fact]
        public void Create_PostInvalidModel_ReturnsView()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BookingController(context);
            controller.ModelState.AddModelError("Name", "Required");
            var booking = new Booking(); // Missing data

            // Act
            var result = controller.Create(booking);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(booking, viewResult.Model);
        }

        // =======================
        // SEARCH BY NAME TEST
        // =======================

        [Fact]
        public void SearchByName_ReturnsMatchingBookings()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Bookings.AddRange(
                new Booking { Name = "Alice", Email = "alice@test.com", ShowDate = DateTime.Now, NumberOfSeats = 1 },
                new Booking { Name = "Bob", Email = "bob@test.com", ShowDate = DateTime.Now, NumberOfSeats = 2 }
            );
            context.SaveChanges();
            var controller = new BookingController(context);

            // Act
            var result = controller.SearchByName("Alice") as JsonResult;
            var bookings = Assert.IsAssignableFrom<IEnumerable<Booking>>(result.Value);

            // Assert
            Assert.Single(bookings);
            Assert.Equal("Alice", bookings.First().Name);
        }

        // =======================
        // GET BOOKING BY ID TEST
        // =======================

        [Fact]
        public void GetBooking_ReturnsBooking_WhenExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var booking = new Booking
            {
                Name = "Charlie",
                Email = "charlie@test.com",
                ShowDate = DateTime.Now,
                NumberOfSeats = 3
            };
            context.Bookings.Add(booking);
            context.SaveChanges();
            var controller = new BookingController(context);

            // Act
            var result = controller.GetBooking(booking.Id) as JsonResult;
            var returnedBooking = Assert.IsType<Booking>(result.Value);

            // Assert
            Assert.Equal("Charlie", returnedBooking.Name);
        }

        // =======================
        // UPDATE BOOKING DATE TEST
        // =======================

        [Fact]
        public void UpdateBookingDate_ChangesDateCorrectly()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var booking = new Booking
            {
                Name = "David",
                Email = "david@test.com",
                ShowDate = DateTime.Today,
                NumberOfSeats = 1
            };
            context.Bookings.Add(booking);
            context.SaveChanges();

            var controller = new BookingController(context);
            var newDate = DateTime.Today.AddDays(5);

            // Act
            var result = controller.UpdateBookingDate(new BookingController.UpdateBookingDto
            {
                Id = booking.Id,
                NewDate = newDate
            }) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newDate, context.Bookings.First().ShowDate);
        }
    }
}
