using Hotel_KYC_Api.Data;
using Hotel_KYC_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_KYC_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ REGISTER HOTEL
        [HttpPost("register")]
        public async Task<IActionResult> RegisterHotel([FromBody] HotelRegistration hotel)
        {
            hotel.CreatedAt = DateTime.UtcNow;

            _context.HotelRegistrations.Add(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }

        // ✅ GET ALL HOTELS (MAIN FIX 🔥)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _context.HotelRegistrations
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();

            return Ok(hotels);
        }

        // ✅ GET HOTEL BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _context.HotelRegistrations.FindAsync(id);

            if (hotel == null)
                return NotFound("Hotel not found");

            return Ok(hotel);
        }

        // ✅ DELETE HOTEL
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.HotelRegistrations.FindAsync(id);

            if (hotel == null)
                return NotFound("Hotel not found");

            _context.HotelRegistrations.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok("Hotel deleted successfully");
        }
    }
}