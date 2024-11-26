using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Services.Interfaces;
using System.Threading.Tasks;

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
	}
}
