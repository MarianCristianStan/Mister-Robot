using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
	public class OrderProduct
    {
		[Required]
		[MaxLength(50)]
		public required string OrderId { get; set; }

		[ForeignKey("OrderId")]
		public Order? Order { get; set; }

		[Required]
		[MaxLength(50)]
		public required string ProductId { get; set; }

		[ForeignKey("ProductId")]
		public Product? Product { get; set; }

		
	}
}