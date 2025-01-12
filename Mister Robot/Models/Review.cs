using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
	public class Review
	{
		[Key]
		[MaxLength(50)]
		public string ReviewId { get; set; } = $"REVIEW-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
		[MaxLength(200)]
		public string? Content { get; set; }

		public required int Rating { get; set; }

		[Required]
		public required string UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }

		[Required]
		[MaxLength(50)]
		public required string ProductId { get; set; }

		[ForeignKey("ProductId")]
		public Product? Product { get; set; }
	}
}
