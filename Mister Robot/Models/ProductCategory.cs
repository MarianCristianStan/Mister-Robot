using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
	public class ProductCategory
	{
		[Key]
		[MaxLength(50)]
		public required string CategoryId { get; set; } = $"CATEGORY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		[MaxLength(50)]
		public required string Name { get; set; }

		[MaxLength(500)]
		public string? Description { get; set; }

		public ICollection<Product>? Products { get; set; }

		[MaxLength(50)]
		public string? ParentCategoryId { get; set; }

		[ForeignKey("ParentCategoryId")]
		public ProductCategory? ParentCategory { get; set; }

		public ICollection<ProductCategory>? SubCategories { get; set; }
	}
}