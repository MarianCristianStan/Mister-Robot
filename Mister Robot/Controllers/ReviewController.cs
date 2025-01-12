using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mister_Robot.Controllers
{
	[Authorize(Roles = "Admin,Basic")]
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;
		private readonly IUserService _userService;
		private readonly IProductService _productService;

		public ReviewController(IReviewService reviewService, IUserService userService, IProductService productService)
		{
			_reviewService = reviewService;
			_userService = userService;
			_productService = productService;
		}

		public async Task<IActionResult> Index(string productId)
		{
			var reviews = _reviewService.GetReviewsByProductId(productId);
			
			ViewData["ProductId"] = productId;
			var product = _productService.GetById(productId);
			ViewBag.ProductTitle = product.Name;

			return View(reviews);
		}

		[HttpPost]
		public IActionResult AddReview(Review review, string productId)
		{
			if (review.Rating <= 0)
			{
				TempData["ErrorMessage"] = "Please select a star rating before submitting your review.";
				return RedirectToAction("Index", "Product", new { id = productId });
			}
			try
			{
				var currentUser = _userService.GetCurrentUser();
				review.User = currentUser;
				review.UserId = currentUser.Id;
				review.ProductId = productId;
				_reviewService.Add(review);
				TempData["SuccessMessage"] = "Thank you for your review!";
				return RedirectToAction("Index", "Product", new { id = productId });
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Product", new { id = productId });
			}
		}

		[HttpPost]
		public IActionResult DeleteReview(string id, string productId)
		{
			try
			{
				_reviewService.Delete(id);
				return RedirectToAction("Index", "Product", new { id = productId });
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Product", new { id = productId });
			}
		}
	}

	
}
