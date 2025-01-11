using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mister_Robot.Models;

namespace Mister_Robot.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

	}
}
