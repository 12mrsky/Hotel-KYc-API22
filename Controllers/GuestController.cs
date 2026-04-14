using Hotel_KYC_Api.Data;
using Hotel_KYC_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_KYC_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuestController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER GUEST =================
        [HttpPost("register-guest")]
        public async Task<IActionResult> RegisterGuest([FromBody] GuestRegistration guest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (guest.HotelId == 0) guest.HotelId = 1;

                _context.GuestRegistrations.Add(guest);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Guest registration completed successfully",
                    guestId = guest.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Database error occurred",
                    details = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        // ================= FLAGGED GUESTS =================
        [HttpGet("flagged-guests")]
        public async Task<IActionResult> GetFlaggedGuests()
        {
            var data = await _context.GuestRegistrations
                .Include(g => g.Hotel)
                .Where(g => g.IsFlagged || g.AadhaarNumber.Length < 12)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync(); // ✅ first fetch

            var result = data.Select(g => new
            {
                id = g.Id,
                guestName = g.GuestName,
                hotelName = g.Hotel != null ? g.Hotel.HotelName : "Unknown Hotel",
                hotelAddress = g.Hotel != null ? g.Hotel.Address : "N/A",
                aadhaarNumber = g.AadhaarNumber,
                roomNumber = g.RoomNumber,
                mobileNumber = g.MobileNumber,
                policeRemarks = g.PoliceRemarks ?? "Suspicious activity detected",

                // ✅ SAFE conversion
                checkInTime = g.CheckInTime?.ToString("yyyy-MM-dd HH:mm:ss"),

                isFlagged = g.IsFlagged
            });

            return Ok(result);
        }

        // ================= ALL GUESTS =================
        [HttpGet("all-guests")]
        public async Task<IActionResult> GetAllGuests()
        {
            try
            {
                var data = await _context.GuestRegistrations
                    .Include(g => g.Hotel)
                    .OrderByDescending(g => g.CreatedAt)
                    .ToListAsync(); // ✅ fetch first

                var result = data.Select(g => new
                {
                    g.Id,
                    g.GuestName,
                    g.RoomNumber,
                    g.MobileNumber,
                    g.AadhaarNumber,
                    HotelName = g.Hotel != null ? g.Hotel.HotelName : "N/A",

                    // ✅ SAFE conversion
                    CheckIn = g.CheckInTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A",
                    CheckOut = g.CheckOutTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Present"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // ================= ALL HOTELS =================
        [HttpGet("all-registered-hotels")]
        public async Task<IActionResult> GetAllRegisteredHotels()
        {
            var hotels = await _context.HotelRegistrations
                .OrderByDescending(h => h.CreatedAt)
                .Select(h => new
                {
                    hotelId = h.Id,
                    hotelName = h.HotelName,
                    ownerName = h.OwnerName,
                    hotelAddress = h.Address,
                    mobileNumber = h.MobileNumber,
                    gstNumber = h.GSTNumber
                })
                .ToListAsync();

            return Ok(hotels);
        }

        // ================= GUESTS BY HOTEL =================
        [HttpGet("by-hotel/{hotelId}")]
        public async Task<IActionResult> GetGuestsByHotel(int hotelId)
        {
            var data = await _context.GuestRegistrations
                .Where(g => g.HotelId == hotelId)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync(); // ✅ fetch first

            var result = data.Select(g => new
            {
                g.GuestName,
                g.RoomNumber,
                g.AadhaarNumber,
                g.MobileNumber,

                // ✅ SAFE conversion
                checkIn = g.CheckInTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"
            });

            return Ok(result);
        }
    }
}

//using Hotel_KYC_Api.Data;
//using Hotel_KYC_Api.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Hotel_KYC_Api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class GuestController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public GuestController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost("register-guest")]
//        public async Task<IActionResult> RegisterGuest([FromBody] GuestRegistration guest)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _context.GuestRegistrations.Add(guest);
//                await _context.SaveChangesAsync();

//                return Ok(new { message = "Guest registration completed successfully", guestId = guest.Id });
//            }
//            catch (Exception ex)
//            {
//                // Log the error here
//                return StatusCode(500, new { message = "Database error occurred", details = ex.Message });
//            }
//        }

//        [HttpGet("all-guests")]
//        public async Task<IActionResult> GetAllGuests()
//        {
//            var guests = await _context.GuestRegistrations.OrderByDescending(g => g.CreatedAt).ToListAsync();
//            return Ok(guests);
//        }
//    }
//}