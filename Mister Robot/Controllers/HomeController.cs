using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Services.Interfaces;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mister_Robot.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserService _userService;

		public HomeController(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			var user = _userService.GetCurrentUser();
			ViewBag.IsAuthenticated = user != null;
			ViewBag.IsAdmin = user != null && await _userService.IsUserAdminAsync(user);

			ViewData["Title"] = "Welcome to Mister Robot";
			ViewData["MetaDescription"] = "Discover the best CPUs, GPUs, and accessories for your electronic needs.";
			ViewData["MetaKeywords"] = "CPUs, GPUs, electronics, accessories, online store";

			return View();
		}

		public IActionResult Login()
		{
			return RedirectToPage("/Account/Login", new { area = "Identity" });
		}

		public IActionResult Register()
		{
			return RedirectToPage("/Account/Register", new { area = "Identity" });
		}

		public IActionResult Sitemap()
		{
			var sitemapNodes = new List<(string Url, DateTime LastModified, string ChangeFrequency, string Priority)>
			 {
				  (Url.Action("Index", "Home", null, Request.Scheme), DateTime.UtcNow.AddDays(-1), "daily", "1.0"),
				  (Url.Action("Index", "Inventory", null, Request.Scheme), DateTime.UtcNow.AddDays(-7), "weekly", "0.8")
			 };

			var xml = new XElement("urlset",
				 sitemapNodes.Select(node =>
					  new XElement("url",
							new XElement("loc", node.Url),
							new XElement("lastmod", node.LastModified.ToString("yyyy-MM-dd")),
							new XElement("changefreq", node.ChangeFrequency),
							new XElement("priority", node.Priority)
					  )
				 )
			);
			return Content(xml.ToString(), "application/xml");
		}

	}
}
