using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
   public class GPU
   {
      [Key]
      public int GPU_ID { get; set; }

      [Required]
      [Range(1, 512)]
      public int MemorySize { get; set; }

      [Required]
      [MaxLength(50)]
      public required string MemoryType { get; set; }

      [Required]
      [Range(1, 5000)]
      public int CoreClock { get; set; } 

      [Required]
      [Range(1, 5500)]
      public int BoostClock { get; set; } 

      public int ProductID { get; set; }
      [ForeignKey("ProductID")]
      public Product Product { get; set; }
   }
}
