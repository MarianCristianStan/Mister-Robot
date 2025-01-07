using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models
{
   public class MisterRobotContext : IdentityDbContext<User>
   {
      public MisterRobotContext(DbContextOptions<MisterRobotContext> options) : base(options)
      {
      }
		public DbSet<Product>? Products { get; set; }
		public DbSet<ProductCategory>? ProductCategories { get; set; }
		public DbSet<Supplier>? Suppliers { get; set; }
		public DbSet<UserAddress>? UserAddresses { get; set; }
		public DbSet<Cart>? Carts { get; set; }
		public DbSet<Wishlist>? Wishlists { get; set; }
		public DbSet<Order>? Orders { get; set; }
		public DbSet<ProductFeature>? ProductFeatures { get; set; }
		public DbSet<CartProduct> CartProducts { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			
			modelBuilder.Entity<CartProduct>()
				.HasKey(cp => new { cp.CartId, cp.ProductId });

			modelBuilder.Entity<CartProduct>()
				.HasOne(cp => cp.Cart)
				.WithMany(c => c.CartProducts)
				.HasForeignKey(cp => cp.CartId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CartProduct>()
				.HasOne(cp => cp.Product)
				.WithMany(p => p.CartProducts)
				.HasForeignKey(cp => cp.ProductId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ProductFeature>()
				.HasKey(pf => new { pf.ProductId, pf.FeatureName });

			modelBuilder.Entity<ProductFeature>()
				.HasOne(pf => pf.Product)
				.WithMany(p => p.Features)
				.HasForeignKey(pf => pf.ProductId);

		
		}
	}



}
