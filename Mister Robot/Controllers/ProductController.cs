using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Controllers
{
	public class ProductController : Controller
	{
      private readonly IUserService _userService;
      private readonly IProductService _productService;
      private readonly IProductCategoryService _productCategoryService;
      private readonly IProductFeatureService _productFeatureService;
		private readonly IFeatureService _featureService;
		public ProductController(IUserService userService, IProductService productService, IProductCategoryService productCategoryService, IProductFeatureService productFeatureService, IFeatureService featureService)
      {
         _userService = userService;
         _productService = productService;
         _productCategoryService = productCategoryService;
         _productFeatureService = productFeatureService;
			_featureService = featureService;
      }

      public IActionResult Index(string id)
      {
         var product = _productService.GetById(id);
         if (product == null)
         {
            return NotFound();
         }

         if (product.ProductCategory == null && !string.IsNullOrEmpty(product.ProductCategoryId))
         {
            product.ProductCategory = _productCategoryService.GetById(product.ProductCategoryId);
         }

         product.ProductFeatures = _productFeatureService.GetFeaturesByProductId(id);

         
         var filteredProducts = _productService.GetAll()
            .Where(p => p.ProductCategoryId == product.ProductCategoryId && p.ProductId != product.ProductId)
            .ToList();

         ViewBag.RelatedProducts = filteredProducts.Any() ? filteredProducts : new List<Mister_Robot.Models.Product>();

         return View(product);
      }


      [HttpGet]
      public IActionResult CompareProducts(string ProductId, string ComparisonProductId)
      {
	      var product1 = _productService.GetById(ProductId);
	      var product2 = _productService.GetById(ComparisonProductId);

	      if (product1 == null || product2 == null)
	      {
		      return NotFound("One or both products not found.");
	      }

	      // Fetch the product features using their IDs
	      var product1Features = _productFeatureService.GetFeaturesByProductId(product1.ProductId)
		      .Select(pf => new ProductFeature
		      {
					ProductId = product1.ProductId,
			      FeatureId = pf.FeatureId,
			      FeatureValue = pf.FeatureValue
		      }).ToList();

	      var product2Features = _productFeatureService.GetFeaturesByProductId(product2.ProductId)
		      .Select(pf => new ProductFeature
		      {
			      ProductId = product2.ProductId,
					FeatureId = pf.FeatureId,
			      FeatureValue = pf.FeatureValue
		      }).ToList();

	      // Fetch all unique feature IDs
	      var allFeatureIds = product1Features.Select(f => f.FeatureId)
		      .Union(product2Features.Select(f => f.FeatureId))
		      .Distinct()
		      .ToList();

	      // Generate a comparison result with feature names fetched dynamically by their ID
	      var comparisonResult = allFeatureIds.Select(featureId =>
	      {
		      var feature = _featureService.GetFeatureById(featureId);
		      var product1Feature = product1Features.FirstOrDefault(f => f.FeatureId == featureId);
		      var product2Feature = product2Features.FirstOrDefault(f => f.FeatureId == featureId);

		      return new
		      {
			      FeatureName = feature.Name,
			      Product1Value = product1Feature?.FeatureValue ?? "N/A",
			      Product2Value = product2Feature?.FeatureValue ?? "N/A"
		      };
	      }).ToList();

	      // Assign the results to the ViewBag
	      ViewBag.Product1 = product1;
	      ViewBag.Product2 = product2;
	      ViewBag.ComparisonResult = comparisonResult;

	      return View("ComparisonResult");
      }




	}
}
