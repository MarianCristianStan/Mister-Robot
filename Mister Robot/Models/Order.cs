using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models
{
	public class Order
	{
		[Key]
		[MaxLength(50)]
		public required string OrderId { get; set; } = $"ORDER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		public required string UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		public ICollection<Product>? Products { get; set; }
      [Precision(18, 2)]
      public decimal TotalAmount { get; set; }
	}

}
