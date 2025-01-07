using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
   public class Supplier
   {
      [Key]
      [MaxLength(50)]
		public required string SupplierId { get; set; } = $"SUPPLIER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
      [MaxLength(100)]
      public required string Name { get; set; }

      [Required]
      [Phone]
      [MaxLength(15)]
		public required string ContactNumber { get; set; }

      [EmailAddress]
      [MaxLength(50)]
		public string? Email { get; set; }

      [Required]
      [MaxLength(250)]
      public required string Address { get; set; }

      public ICollection<Product>? ProductsSupplied { get; set; }
   }
}
