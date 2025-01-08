using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
	public class WishlistProduct
    {
		[Required]
		[MaxLength(50)]
		public required string WishlistId { get; set; }

		[ForeignKey("WishlistId")]
		public Wishlist? Wishlist { get; set; }

		[Required]
		[MaxLength(50)]
		public required string ProductId { get; set; }

		[ForeignKey("ProductId")]
		public Product? Product { get; set; }

		
	}
}