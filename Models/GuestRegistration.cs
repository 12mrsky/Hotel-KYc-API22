// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace Hotel_KYC_Api.Models
// {
//     public class GuestRegistration
//     {
//         [Key]
//         public int Id { get; set; }

//         [Required]
//         public int HotelId { get; set; }

//         [ForeignKey("HotelId")]
//         public virtual HotelRegistration? Hotel { get; set; }

//         [Required]
//         public string RoomNumber { get; set; } = string.Empty;

//         [Required]
//         public string GuestName { get; set; } = string.Empty;

//         public DateTime? CheckInTime { get; set; }
//         public DateTime? CheckOutTime { get; set; }

//         public int Adults { get; set; } = 1;
//         public int Kids { get; set; } = 0;

//         [Required]
//         [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhaar must be 12 digits.")]
//         public string AadhaarNumber { get; set; } = string.Empty;

//         public int Age { get; set; }

//         [Required]
//         public string MobileNumber { get; set; } = string.Empty;

//         public string? Address { get; set; }
//         public string? ComingFrom { get; set; }
//         public string? GoingTo { get; set; }

//         // FIX: Use UTC time (important for PostgreSQL)
//         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

//         public bool IsFlagged { get; set; } = false;

//         public string? PoliceRemarks { get; set; }

//         public string Status { get; set; } = "Checked-In";
//     }
// }
// My Old Code (for reference)
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_KYC_Api.Models
{
    public class GuestRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public virtual HotelRegistration? Hotel { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string GuestName { get; set; } = string.Empty;

        // ✅ Nullable DateTime (correct)
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public int Adults { get; set; } = 1;
        public int Kids { get; set; } = 0;

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string AadhaarNumber { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? ComingFrom { get; set; }
        public string? GoingTo { get; set; }

        // ✅ UTC fix
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsFlagged { get; set; } = false;

        public string? PoliceRemarks { get; set; }

        public string Status { get; set; } = "Checked-In";
    }
}