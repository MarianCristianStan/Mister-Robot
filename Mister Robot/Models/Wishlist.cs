using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
	public class Wishlist
	{
        [Key]
		[MaxLength(50)]
		public string WishlistId { get; set; } = $"WISHLIST-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		public required string UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }

		public ICollection<WishlistProduct>? WishlistProducts { get; set; } = new List<WishlistProduct>();


    }
}
