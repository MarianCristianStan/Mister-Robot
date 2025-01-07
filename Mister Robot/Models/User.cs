using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Mister_Robot.Models
{
   public class User : IdentityUser
   {
      [Required]
      [MaxLength(50)]
      public required string FirstName { get; set; }

      [Required]
      [MaxLength(50)]
      public required string LastName { get; set; }

      public byte[]? ProfilePicture { get; set; }

		public UserAddress? UserAddress { get; set; }
		public  Cart? Cart { get; set; }
		public  Wishlist? Wishlist { get; set; }

		public ICollection<Order>? Orders { get; set; }
	}
}


