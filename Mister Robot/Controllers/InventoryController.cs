using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Models;
using Mister_Robot.Services;
using Mister_Robot.Services.Interfaces;
using System.Collections.Generic;

namespace Mister_Robot.Controllers
{
   public class InventoryController : Controller
   {
      private readonly IProductService _productService;
      private readonly IProductCategoryService _productCategoryService;
      private readonly IUserService _userService;
      private readonly ISupplierService _supplierService;
      private readonly IFeatureService _featureService;
      private readonly IProductFeatureService _productFeatureService;

		public InventoryController(IProductService productService, IUserService userService, IProductCategoryService productCategoryService, ISupplierService supplierService,IFeatureService featureService, IProductFeatureService productFeatureService )
      {
         _productService = productService;
         _userService = userService;
         _productCategoryService = productCategoryService;
         _supplierService = supplierService;
         _featureService = featureService;
         _productFeatureService = productFeatureService;

      }
      public IActionResult Index(string search = null, string category = null)
      {
         List<Product> products = _productService.GetAll();
         List<ProductCategory> categories = _productCategoryService.GetAll();
         if (categories == null || !categories.Any())
         {
            categories = new List<ProductCategory>();
         }
         ViewBag.Categories = categories;


         if (!string.IsNullOrEmpty(category))
         {
            products = products.Where(p => p.ProductCategoryId.ToString() == category).ToList();
            ViewBag.SelectedCategory = category;
         }


         if (!string.IsNullOrEmpty(search))
         {
            products = products.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (p.Description != null && p.Description.Contains(search, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            ViewBag.SearchTerm = search;
         }
         foreach (var product in products)
         {
            product.ProductCategory = _productCategoryService.GetById(product.ProductCategoryId);
         }
         return View(products);
      }


      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult Manage()
      {
         var products = _productService.GetAll();
         foreach (var product in products)
         {
            product.ProductCategory = _productCategoryService.GetById(product.ProductCategoryId);
            product.Supplier = _supplierService.GetById(product.SupplierId);
         }
         return View(products);
      }

      // Create Product Form
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult AddProduct()
      {
         ViewBag.Categories = _productCategoryService.GetAll();
         ViewBag.Suppliers = _supplierService.GetAll();
         return View();
      }

      
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult AddProduct(Product product, IFormFile ImageFile)
      {
			if (ImageFile != null && ImageFile.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					ImageFile.CopyTo(ms);
					product.Image = ms.ToArray();
				}
			}

			if (ModelState.IsValid)
         {
            _productService.Add(product);
            return RedirectToAction("Manage");
         }

         ViewBag.Categories = _productCategoryService.GetAll();
         ViewBag.Suppliers = _supplierService.GetAll();
         return View(product);
      }

      
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult EditProduct(string id)
      {
         var product = _productService.GetById(id);

			if (product == null)
         {
            return NotFound();
         }
			ViewBag.Suppliers = _supplierService.GetAll();
			ViewBag.Categories = _productCategoryService.GetAll();
         return View(product);
      }

      // Handle Product Updates
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult EditProduct(Product product, IFormFile ImageFile = null)
      {
         
         var existingProduct = _productService.GetById(product.ProductId);
         if (existingProduct == null)
         {
            return NotFound();
         }

    
         if (ImageFile != null && ImageFile.Length > 0)
         {
            using (var ms = new MemoryStream())
            {
               ImageFile.CopyTo(ms);
               product.Image = ms.ToArray();
            }
         }
         else
         {
        
            product.Image = existingProduct.Image;
         }

         if (ModelState.IsValid)
         {
        
            _productService.Update(product);
            return RedirectToAction("Manage");
         }

        
         ViewBag.Suppliers = _supplierService.GetAll();
         ViewBag.Categories = _productCategoryService.GetAll();

        
         return View(product);
      }


      // Delete Product
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult Delete(string id)
      {
         _productService.Delete(id);
         return RedirectToAction("Manage");
      }


      [HttpGet]
      public IActionResult LinkProductFeatures(string id)
      {
	      var product = _productService.GetById(id);
	      if (product == null)
	      {
		      return NotFound();
	      }

	      ViewBag.Features = _featureService.GetAll();
	      ViewBag.LinkedFeatures = _productFeatureService.GetFeaturesByProductId(id);
			return View(product);
      }


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult LinkProductFeatures(string productId, string featureId, string featureValue)
		{
			// Check if the feature already exists for the product
			var existingLink = _productFeatureService.GetAll()
				.FirstOrDefault(pf => pf.ProductId == productId && pf.FeatureId == featureId);

			if (existingLink != null)
			{
				// Return the view with an error message without redirecting
				ViewBag.ErrorMessage = "This feature is already linked to the product.";
				var product = _productService.GetById(productId);
				ViewBag.Features = _featureService.GetAll().Where(f => f.ProductCategoryId == product.ProductCategoryId).ToList();
				ViewBag.LinkedFeatures = _productFeatureService.GetFeaturesByProductId(productId);
				return View(product);
			}

			// If no conflict, add the new feature
			var productFeature = new ProductFeature
			{
				ProductId = productId,
				FeatureId = featureId,
				FeatureValue = featureValue
			};

			_productFeatureService.Add(productFeature);

			// Return to the same page with a success message
			ViewBag.SuccessMessage = "Feature successfully linked!";
			var updatedProduct = _productService.GetById(productId);
			ViewBag.Features = _featureService.GetAll().Where(f => f.ProductCategoryId == updatedProduct.ProductCategoryId).ToList();
			ViewBag.LinkedFeatures = _productFeatureService.GetFeaturesByProductId(productId);
			return View(updatedProduct);
		}

      [HttpPost]
      public IActionResult UnlinkProductFeature(string productId, string featureId)
      {
         try
         {
            // Use the DeleteCompositeKey method for better deletion management
            _productFeatureService.DeleteCompositeKey(productId, featureId);

            ViewBag.SuccessMessage = "Feature unlinked successfully!";
         }
         catch (Exception ex)
         {
            ViewBag.ErrorMessage = $"Error while unlinking feature: {ex.Message}";
         }

      
         var product = _productService.GetById(productId);
         ViewBag.Features = _featureService.GetAll();
         ViewBag.LinkedFeatures = _productFeatureService.GetFeaturesByProductId(productId);
         return View("LinkProductFeatures", product);
      }


   }
}
