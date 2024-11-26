using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
   public class UserAddress
   {
      [Key]
      public int UserAddressID { get; set; }

      [Required]
      [MaxLength(50)]
      public required string City { get; set; }

      [Required]
      [MaxLength(50)]
      public required string Country { get; set; }

      [Required]
      [MaxLength(100)]
      public required string Street { get; set; }

      [Required]
      [MaxLength(10)]
      public required string PostalCode { get; set; }

      [Phone]
      public string? PhoneNumber { get; set; }

      [Required]
      public required string UserId { get; set; }
      public User? User { get; set; }
   }
}
