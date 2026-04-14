// using Hotel_KYC_Api.Data;
// using Hotel_KYC_Api.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace Hotel_KYC_Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AuthController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public AuthController(AppDbContext context)
//         {
//             _context = context;
//         }
//         [HttpPost("register")]
// public async Task<IActionResult> Register([FromBody] RegisterRequest request)
// {
//     // 1. Check if the email already exists before trying to save
//     var existingUser = await _context.Users.AnyAsync(u => u.Email == request.Email);
//     if (existingUser)
//     {
//         return BadRequest(new { message = "Email is already registered." });
//     }

//     try 
//     {
//         var user = new User
//         {
//             FullName = request.FullName,
//             Email = request.Email,
//             PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
//             PhoneNumber = request.PhoneNumber,
//             CreatedAt = DateTime.Now
//         };

//         _context.Users.Add(user);
//         await _context.SaveChangesAsync();

//         return Ok(new { message = "User registered successfully", userId = user.UserId });
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, new { message = "Database error", details = ex.Message });
//     }
// }
//         //[HttpPost("register")]
//         //public async Task<IActionResult> Register([FromBody] RegisterRequest request)
//         //{
//         //    var user = new User
//         //    {
//         //        FullName = request.FullName,
//         //        Email = request.Email,
//         //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
//         //        PhoneNumber = request.PhoneNumber
//         //    };

//         //    _context.Users.Add(user);
//         //    await _context.SaveChangesAsync();

//         //    return Ok(new { message = "User registered successfully", userId = user.UserId });
//         //}

//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginRequest request)
//         {
//             // 1. Search for the user by FullName (Username)
//             var user = await _context.Users.FirstOrDefaultAsync(u => u.FullName == request.FullName);

//             // 2. Verify if user exists and if the password matches the hash
//             if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
//             {
//                 return Unauthorized(new { message = "Invalid Username or Password" });
//             }

//             // 3. Return successful login data
//             return Ok(new
//             {
//                 message = "Login successful",
//                 userId = user.UserId,
//                 fullName = user.FullName,
//                 email = user.Email,
//                 role = user.Role 
//             });
//         }

//         public class LoginRequest
//         {
//             public string FullName { get; set; }
//             public string Password { get; set; }
//         }

//     }
// }

// using Hotel_KYC_Api.Data;
// using Hotel_KYC_Api.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace Hotel_KYC_Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AuthController : ControllerBase
//     {
//         private readonly AppDbContext _context;

//         public AuthController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // ================= REGISTER =================
//         [HttpPost("register")]
//         public async Task<IActionResult> Register([FromBody] RegisterRequest request)
//         {
//             // ✅ Check if email exists (case-insensitive)
//             var existingUser = await _context.Users
//                 .AnyAsync(u => u.Email.ToLower() == request.Email.ToLower());

//             if (existingUser)
//             {
//                 return BadRequest(new { message = "Email is already registered." });
//             }

//             try
//             {
//                 var user = new User
//                 {
//                     FullName = request.FullName,
//                     Email = request.Email,
//                     PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
//                     PhoneNumber = request.PhoneNumber,

//                     // 🔥 FIX: Use UTC for PostgreSQL
//                     CreatedAt = DateTime.UtcNow,

//                     // 🔥 IMPORTANT: default role (if not provided)
//                     Role = "Hotel"
//                 };

//                 _context.Users.Add(user);
//                 await _context.SaveChangesAsync();

//                 return Ok(new
//                 {
//                     message = "User registered successfully",
//                     userId = user.UserId
//                 });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new
//                 {
//                     message = "Database error",
//                     details = ex.InnerException?.Message ?? ex.Message
//                 });
//             }
//         }

//         // ================= LOGIN =================
//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginRequest request)
//         {
//             // ✅ Case-insensitive username search
//             var user = await _context.Users
//                 .FirstOrDefaultAsync(u => u.FullName.ToLower() == request.FullName.ToLower());

//             // ❌ Invalid login
//             if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
//             {
//                 return Unauthorized(new { message = "Invalid Username or Password" });
//             }

//             // ✅ Return role-based data
//             return Ok(new
//             {
//                 message = "Login successful",
//                 userId = user.UserId,
//                 fullName = user.FullName,
//                 email = user.Email,

//                 // 🔥 IMPORTANT (fallback safety)
//                 role = user.Role ?? "Hotel"
//             });
//         }

//         // ================= LOGIN REQUEST MODEL =================
//         public class LoginRequest
//         {
//             public string FullName { get; set; } = string.Empty;
//             public string Password { get; set; } = string.Empty;
//         }
//     }
// }

// MY OLD codes
using Hotel_KYC_Api.Data;
using Hotel_KYC_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_KYC_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ REGISTER (FIXED)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and Password are required");

            var exists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (exists)
                return BadRequest("User already exists");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,

                // 🔐 HASH PASSWORD
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),

                CreatedAt = DateTime.UtcNow,

                // 🔥 DEFAULT ROLE
                Role = "Hotel"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully",
                user.Email,
                user.Role
            });
        }

        // ✅ LOGIN (ROLE BASED)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == login.Email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                message = "Login successful",
                userId = user.UserId,
                fullName = user.FullName,
                email = user.Email,
                role = user.Role ?? "Hotel"
            });
        }
    }

    // ✅ LOGIN DTO
    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}