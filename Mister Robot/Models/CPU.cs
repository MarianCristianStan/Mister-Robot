using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mister_Robot.Models
{
   public class CPU
   {
      [Key]
      public int CPU_ID { get; set; }

      [Required]
      [Range(1, 128)]
      public int CoreCount { get; set; }

      [Required]
      [Range(1, 256)]
      public int ThreadCount { get; set; }

      [Required]
      [Range(0.1, 10)]
		[Precision(18, 4)]
		public decimal BaseClock { get; set; } 

      [Required]
      [Range(0.1, 10)]
		[Precision(18, 4)]
		public decimal BoostClock { get; set; } 

      [Required]
      [MaxLength(50)]
      public required string SocketType { get; set; }

      public int ProductID { get; set; }
      [ForeignKey("ProductID")]
      public Product Product { get; set; }
   }
}
