using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
   public class Product
   {
      [Key]
      public int ProductID { get; set; }

      [Required]
      [MaxLength(100)]
      public required string Name { get; set; }

      [Required]
      [MaxLength(50)]
      public required string Brand { get; set; }

		public byte[]? Image { get; set; }

      [MaxLength(500)]
      public string? Description { get; set; } 

		[Required]
      [Range(0, double.MaxValue)]
		[Precision(18, 2)]
		public decimal Price { get; set; }

      [Required]
      [Range(0, int.MaxValue)]
      public int StockQuantity { get; set; }

      [Required]
      public int SupplierID { get; set; }
      public Supplier? Supplier { get; set; }

      public int ProductCategoryId { get; set; }

      public ProductCategory? ProductCategory { get; set; }

		public CPU? CPU { get; set; }
		public GPU? GPU { get; set; }
	}
}
