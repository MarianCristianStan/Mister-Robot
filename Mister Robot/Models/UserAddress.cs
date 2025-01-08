using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
   public class UserAddress
   {
      [Key]
		[MaxLength(50)]
		public string UserAddressId { get; set; }  = $"ADDRESS-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

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
      [MaxLength(15)]
		public string? PhoneNumber { get; set; }

      [Required]
		public required string UserId { get; set; }

      [ForeignKey("UserId")]
		public User? User { get; set; }
   }
}
