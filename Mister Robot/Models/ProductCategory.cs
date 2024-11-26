using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
   public class ProductCategory
   {
      [Key]
      public int Id { get; set; }

      [Required]
      [MaxLength(50)]
      public required string Name { get; set; }

      [MaxLength(500)]
      public string? Description { get; set; }

      public ICollection<Product>? Products { get; set; }
   }
}
