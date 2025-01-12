using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Services.Interfaces;
using System.Security.Claims;

namespace Mister_Robot.Controllers
{
	[Authorize]
	public class WishlistController : Controller
   {
      private readonly IWishlistService _wishlistService;
      private readonly IWishlistProductService _wishlistProductService;
      private readonly IProductService _productService;
      private readonly IUserService _userService;

      public WishlistController(IWishlistService wishlistService, IProductService productService, IUserService userService, IWishlistProductService wishlistProductService)
      {
         _wishlistService = wishlistService;
         _wishlistProductService = wishlistProductService;
         _productService = productService;
         _userService = userService;
      }

      [Authorize]
      public IActionResult Index()
      {
         var wishlistItems = _wishlistService.GetWishlistItems().Select(wishlistProduct =>
         {
            wishlistProduct.Product = _productService.GetById(wishlistProduct.ProductId);
            return wishlistProduct;
         }).ToList();

         return View(wishlistItems);
      }

      [HttpPost]
      [Authorize]
      public IActionResult AddToWishlist(string productId)
      {
         if (string.IsNullOrEmpty(productId))
         {
            return BadRequest("Invalid product ID.");
         }

         _wishlistService.AddToWishlist(productId);
         return RedirectToAction("Index");
      }

      [HttpPost]
      [Authorize]
      public IActionResult RemoveItem(string productId)
      {
         if (string.IsNullOrEmpty(productId))
         {
            return BadRequest("Invalid product ID.");
         }

         _wishlistService.RemoveFromWishlist(productId);
         return RedirectToAction("Index");
      }

      [HttpPost]
      [Authorize]
      public IActionResult ToggleProductWishlist(string productId)
      {
         if (string.IsNullOrEmpty(productId))
         {
            return BadRequest("Invalid product ID.");
         }

         var userId = _userService.GetCurrentUser().Id;

         var wishlist = userId != null ? _wishlistService.GetWishlistByUserId(userId) : null;

         var isProductInWishlist = _wishlistProductService
	         .GetWishlistProductsByWishlistId(wishlist.WishlistId)
	         .Any(wp => wp.ProductId == productId);

			if (isProductInWishlist)
         {
            _wishlistService.RemoveFromWishlist(productId);
         }
         else
         {
            _wishlistService.AddToWishlist(productId);
         }

         return RedirectToAction("Index");
      }

   }
}