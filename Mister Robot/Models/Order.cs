using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models
{
	public class Order
	{
		[Key]
		[MaxLength(50)]
		public string OrderId { get; set; } = $"ORDER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		public required string UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }

		[Required]
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required] public required string Status { get; set; } = "Pending";

		public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
	}

}
