using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models;

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
	public DbSet<Feature> Features { get; set; }
	public DbSet<ProductFeature>? ProductFeatures { get; set; }
	public DbSet<CartProduct> CartProducts { get; set; }
	public DbSet<WishlistProduct> WishlistProducts { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// CartProduct Configuration
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
			.OnDelete(DeleteBehavior.Cascade);

		// WishlistProduct Configuration
		modelBuilder.Entity<WishlistProduct>()
			.HasKey(wp => new { wp.WishlistId, wp.ProductId });

		modelBuilder.Entity<WishlistProduct>()
			.HasOne(wp => wp.Wishlist)
			.WithMany(w => w.WishlistProducts)
			.HasForeignKey(wp => wp.WishlistId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<WishlistProduct>()
			.HasOne(wp => wp.Product)
			.WithMany(p => p.WishlistProducts)
			.HasForeignKey(wp => wp.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		// OrderProduct Configuration
		modelBuilder.Entity<OrderProduct>()
			.HasKey(op => new { op.OrderId, op.ProductId });

		modelBuilder.Entity<OrderProduct>()
			.HasOne(op => op.Order)
			.WithMany(o => o.OrderProducts)
			.HasForeignKey(op => op.OrderId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<OrderProduct>()
			.HasOne(op => op.Product)
			.WithMany(p => p.OrderProducts)
			.HasForeignKey(op => op.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<ProductFeature>()
			.HasKey(pf => new { pf.ProductId, pf.FeatureId });

		modelBuilder.Entity<ProductFeature>()
			.HasOne(pf => pf.Product)
			.WithMany(p => p.ProductFeatures)
			.HasForeignKey(pf => pf.ProductId)
			.OnDelete(DeleteBehavior.Cascade);
		;

		modelBuilder.Entity<ProductFeature>()
			.HasOne(pf => pf.Feature)
			.WithMany(f => f.ProductFeatures)
			.HasForeignKey(pf => pf.FeatureId)
			.OnDelete(DeleteBehavior.NoAction);
		;
	}
}