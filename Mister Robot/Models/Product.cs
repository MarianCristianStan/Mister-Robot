using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Models;

public class Product
{
    [Key]
    [MaxLength(50)]
    public string ProductId { get; set; } = $"PRODUCT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

    [Required] 
    [MaxLength(100)] 
    public required string Name { get; set; }

    [Required] 
    [MaxLength(50)] 
    public required string Brand { get; set; }

    public byte[]? Image { get; set; }

    [MaxLength(500)] public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    [Precision(18, 2)]
    public decimal Price { get; set; }


    [Required] 
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Required] 
    [MaxLength(50)] 
    public required string SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public Supplier? Supplier { get; set; }

    [Required] [MaxLength(50)] 
    public required string ProductCategoryId { get; set; }

    [ForeignKey("ProductCategoryId")] 
    public ProductCategory? ProductCategory { get; set; }

    public ICollection<ProductFeature>? Features { get; set; } = new List<ProductFeature>();

    public ICollection<CartProduct>? CartProducts { get; set; } = new List<CartProduct>();
    public ICollection<WishlistProduct>? WishlistProducts { get; set; } = new List<WishlistProduct>();
    public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
}