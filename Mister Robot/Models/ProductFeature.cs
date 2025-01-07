using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
	public class ProductFeature
	{
		[Key]
		[Column(Order = 0)]
		[MaxLength(50)]
		public required string ProductId { get; set; }

		[Key]
		[Column(Order = 1)]
		[MaxLength(100)]
		public required string FeatureName { get; set; }

		[Required]
		[MaxLength(100)]
		public required string FeatureValue { get; set; }

		[ForeignKey("ProductId")]
		public required Product Product { get; set; }
	}
}
