using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
	public class Feature
	{
		[Key]
		[MaxLength(50)]
		public string FeatureId { get; set; } = $"FEATURE-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required, MaxLength(100)]
		public required string Name { get; set; }

		[Required]
		[MaxLength(50)]
		public required string ProductCategoryId { get; set; }

		[ForeignKey("ProductCategoryId")]
		public ProductCategory? ProductCategory { get; set; }

		public ICollection<ProductFeature> ProductFeatures { get; set; } = new List<ProductFeature>();
	}

}
