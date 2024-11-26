using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
   public class Supplier
   {
      [Key]
      public int SupplierID { get; set; }

      [Required]
      [MaxLength(100)]
      public required string Name { get; set; }

      [Required]
      [Phone]
      public required string ContactNumber { get; set; }

      [EmailAddress]
      public string? Email { get; set; }

      [Required]
      [MaxLength(250)]
      public required string Address { get; set; }

      public ICollection<Product>? ProductsSupplied { get; set; }
   }
}
