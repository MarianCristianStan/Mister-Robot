using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
	public class Cart
	{
		[Key]
		[MaxLength(50)]
		public string CartId { get; set; } = $"CART-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		public required string UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }

		public ICollection<CartProduct>? CartProducts { get; set; } = new List<CartProduct>();

		[NotMapped]
		[Precision(18, 2)]
		public decimal TotalPrice => CartProducts?.Sum(cp => cp.Quantity * cp.Product.Price) ?? 0; // Dynamically calculate total price
	}

}
