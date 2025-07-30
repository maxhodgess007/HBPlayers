using Microsoft.AspNetCore.Mvc;

namespace HerveyPlayersBooking.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Hardcoded credentials for now
            if (username == "admin" && password == "password")
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToAction("ManageBookings", "Booking");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
