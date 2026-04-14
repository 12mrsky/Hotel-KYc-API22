// using System.ComponentModel.DataAnnotations;

// namespace Hotel_KYC_Api.Models
// {
//     public class HotelRegistration
//     {
//         [Key]
//         public int Id { get; set; }

//         [Required]
//         public string HotelName { get; set; } = string.Empty;

//         [Required]
//         public string OwnerName { get; set; } = string.Empty;

//         public string? GSTNumber { get; set; }

//         [Required]
//         public string MobileNumber { get; set; } = string.Empty;

//         [Required]
//         public string Address { get; set; } = string.Empty;

//         //  FIX: Use UTC time (important for PostgreSQL)
//         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//     }
// }

// My Old Code (for reference)
using System.ComponentModel.DataAnnotations;

namespace Hotel_KYC_Api.Models
{
    public class HotelRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string HotelName { get; set; } = string.Empty;

        [Required]
        public string OwnerName { get; set; } = string.Empty;

        public string? GSTNumber { get; set; }

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        // ✅ UTC safe
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}