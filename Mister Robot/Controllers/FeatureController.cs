using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;
using System.Collections.Generic;

namespace Mister_Robot.Controllers
{
	public class FeatureController : Controller
	{
		private readonly IFeatureService _featureService;
		private readonly IProductCategoryService _productCategoryService;

		public FeatureController(IFeatureService featureService, IProductCategoryService productCategoryService)
		{
			_featureService = featureService;
			_productCategoryService = productCategoryService;
		}

		public IActionResult Index()
		{
			var features = _featureService.GetAll();
			return View(features);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Manage()
		{
			var features = _featureService.GetAll();
			return View(features);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult AddFeature()
		{
			ViewBag.Categories = _productCategoryService.GetAll();
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult AddFeature(Feature feature)
		{
			if (ModelState.IsValid)
			{
				_featureService.Add(feature);
				return RedirectToAction("Manage");
			}

			ViewBag.Categories = _productCategoryService.GetAll();
			return View(feature);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult EditFeature(string id)
		{
			var feature = _featureService.GetById(id);
			if (feature == null)
			{
				return NotFound();
			}

			ViewBag.Categories = _productCategoryService.GetAll();
			return View(feature);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult EditFeature(Feature feature)
		{
			if (ModelState.IsValid)
			{
				_featureService.Update(feature);
				return RedirectToAction("Manage");
			}

			ViewBag.Categories = _productCategoryService.GetAll();
			return View(feature);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(string id)
		{
			_featureService.Delete(id);
			return RedirectToAction("Manage");
		}
	}
}
