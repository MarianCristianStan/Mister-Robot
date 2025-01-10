using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Models;
using Mister_Robot.Services;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Controllers
{
	[Authorize]
	public class CartController : Controller
   {
      private readonly IStripeService _stripeService;
      private readonly ICartService _cartService;
      private readonly IProductService _productService;
      private readonly IOrderService _orderService;
		private readonly IUserService _userService;

      public CartController(IStripeService stripeService, ICartService cartService, IProductService productService, IOrderService orderService, IUserService userService)
      {
         _stripeService = stripeService;
         _cartService = cartService;
         _productService = productService;
         _orderService = orderService;
			_userService = userService;
      }
      [Authorize]
		public IActionResult Index()
      {
			var cartItems = _cartService.GetCartItems().Select(cartProduct =>
			{
				cartProduct.Product = _productService.GetById(cartProduct.ProductId);
				return cartProduct;
			}).ToList();

			return View(cartItems);
		}

      [HttpPost]
      [Authorize]
      public IActionResult AddToCart(string productId)
      {
         if (string.IsNullOrEmpty(productId))
         {
            return BadRequest("Invalid product or quantity.");
         }

         _cartService.AddToCart(productId);
         return RedirectToAction("Index", "Inventory");
      }

      [HttpPost]
      [Authorize]
		public IActionResult UpdateQuantity(string productId, int quantity)
      {
	      if (string.IsNullOrEmpty(productId) || quantity < 1)
	      {
		      return BadRequest("Invalid quantity.");
	      }

	      _cartService.UpdateCartQuantity(productId, quantity);
			/* return RedirectToAction("Index");*/
			return Ok();
		}

      [HttpPost]
      [Authorize]
		public IActionResult RemoveItem(string productId)
      {
	      if (string.IsNullOrEmpty(productId))
	      {
		      return BadRequest("Invalid product ID.");
	      }

	      _cartService.RemoveFromCart(productId);
	      return RedirectToAction("Index");
      }

		[HttpPost]
		[Authorize]
		public IActionResult Checkout()
		{
			var cartItems = _cartService.GetCartItems().Select(cp =>
			{
				cp.Product = _productService.GetById(cp.ProductId);
				return cp;
			}).ToList();
			 
			var lineItems = cartItems.Select(cartItem => new Stripe.Checkout.SessionLineItemOptions
			{
				PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
				{
					Currency = "usd",
					ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
					{
						Name = cartItem.Product.Name
						
					},
					UnitAmount = (long)(cartItem.Product.Price * 100)
				},
				Quantity = cartItem.Quantity
			}).ToList();

			var successUrl = Url.Action("OrderSuccess", "Cart", null, Request.Scheme);
			var cancelUrl = Url.Action("Index", "Cart", null, Request.Scheme);

			var checkoutSessionUrl = _stripeService.CreateCheckoutSession(lineItems, successUrl, cancelUrl);
			return Redirect(checkoutSessionUrl);
		}


		[Authorize]
      public IActionResult OrderSuccess()
      {
         var user = _userService.GetCurrentUser();
         var cartItems = _cartService.GetCartItems().Select(cp =>
         {
            cp.Product = _productService.GetById(cp.ProductId);
            return cp;
         }).ToList();

         var newOrder = new Order
         {
            UserId = user.Id,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            TotalAmount = cartItems.Sum(cp => cp.Quantity * cp.Product.Price),
            OrderProducts = cartItems.Select(cp => new OrderProduct
            {
               OrderId = Guid.NewGuid().ToString(), // This will be overwritten when saved
               ProductId = cp.ProductId,
               Quantity = cp.Quantity
            }).ToList()
         };
         _orderService.CreateOrder(newOrder);
         _cartService.ClearCart();

			return View();
      }

    /*  [Authorize]
      public IActionResult OrderSuccess()
      {
         _cartService.ClearCart();
         return View();
      }*/
   }
}
