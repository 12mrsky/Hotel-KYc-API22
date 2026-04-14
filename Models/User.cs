// using System.ComponentModel.DataAnnotations;

// namespace Hotel_KYC_Api.Models
// {
//     public class User
//     {
//         public int UserId { get; set; }   // Identity column

//         [Required]
//         public string FullName { get; set; } = string.Empty;

//         [Required]
//         [EmailAddress]
//         public string Email { get; set; } = string.Empty;

//         [Required]
//         public string PasswordHash { get; set; } = string.Empty;

//         public string? PhoneNumber { get; set; }

//         // 🔥 FIX: Use UTC instead of Local time
//         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

//         // 🔥 Make nullable to avoid DB crash if not provided
//         public string? Role { get; set; }
//     }

//     public class RegisterRequest
//     {
//         [Required]
//         public string FullName { get; set; } = string.Empty;

//         [Required]
//         [EmailAddress]
//         public string Email { get; set; } = string.Empty;

//         [Required]
//         [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
//         public string Password { get; set; } = string.Empty;

//         public string? PhoneNumber { get; set; }
//     }
// }


// My Old Code (for reference)
using System.ComponentModel.DataAnnotations;

namespace Hotel_KYC_Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔥 IMPORTANT: Role from frontend
        public string? Role { get; set; } = "User";
    }

    // ✅ UPDATED DTO (ADD ROLE HERE)
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // 🔥 ADD THIS (MOST IMPORTANT)
        public string Role { get; set; } = "User";
    }
}