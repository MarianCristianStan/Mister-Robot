using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models
{
   public class MisterRobotContext : IdentityDbContext<User>
   {
      public MisterRobotContext(DbContextOptions<MisterRobotContext> options) : base(options)
      {
      }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Product to CPU (One-to-One)
			builder.Entity<CPU>()
				 .HasOne(cpu => cpu.Product)
				 .WithOne(product => product.CPU)
				 .HasForeignKey<CPU>(cpu => cpu.ProductID)
				 .OnDelete(DeleteBehavior.Cascade);

			// Product to GPU (One-to-One)
			builder.Entity<GPU>()
				 .HasOne(gpu => gpu.Product)
				 .WithOne(product => product.GPU)
				 .HasForeignKey<GPU>(gpu => gpu.ProductID)
				 .OnDelete(DeleteBehavior.Cascade);
		}

	
		public DbSet<Product> Products { get; set; }
      public DbSet<ProductCategory> ProductCategories { get; set; }
      public DbSet<Supplier> Suppliers { get; set; }
      public DbSet<UserAddress> UserAddresses { get; set; }
      public DbSet<CPU> CPUs { get; set; }
      public DbSet<GPU> GPUS { get; set; }
   }
}
