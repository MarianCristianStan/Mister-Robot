using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Controllers
{
	public class ContactController : Controller
	{
		private readonly IContactMessageService _contactMessageService;
		private readonly IUserService _userService;
		public ContactController(IContactMessageService contactMessageService, IUserService userService)
		{
			_contactMessageService = contactMessageService;
			_userService = userService;
		}

		
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		
		[HttpPost]
		public IActionResult SubmitMessage(ContactMessage contactMessage)

		{
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Failed!";
				return RedirectToAction("Index");
			}

			contactMessage.SentAt = DateTime.UtcNow;
			contactMessage.IsReplied = false;

			if (User.Identity?.IsAuthenticated == true)
			{
				contactMessage.Username = User.Identity.Name;
			}

			_contactMessageService.Add(contactMessage);

			TempData["SuccessMessage"] = "Your message has been sent successfully!";
			return RedirectToAction("Index");
		}

	
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult AdminReply()
		{
			var allMessages = _contactMessageService.GetAll();
			return View(allMessages);
		}

		// Admin response to a message
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult RespondToMessage(string id)
		{
			var message = _contactMessageService.GetContactMessageById(id);
			if (message == null)
			{
				return NotFound("Message not found.");
			}
			return View(message);
		}

      
      [HttpPost]
      [Authorize(Roles = "Admin")]
      [ValidateAntiForgeryToken]
      public IActionResult SubmitAdminReply(string contactMessageId, string reply)
      {
         if (string.IsNullOrWhiteSpace(reply))
         {
            TempData["ErrorMessage"] = "Reply cannot be empty.";
            return RedirectToAction("RespondToMessage", new { id = contactMessageId });
         }

         var message = _contactMessageService.GetContactMessageById(contactMessageId);
         if (message == null)
         {
            TempData["ErrorMessage"] = "Message not found.";
            return RedirectToAction("AdminReply");
         }

         message.AdminReply = reply;
         message.IsReplied = true;
         message.RespondedBy = User.Identity.Name;
         _contactMessageService.Update(message);

         TempData["SuccessMessage"] = "Reply sent successfully!";
         return RedirectToAction("AdminReply");
      }


      [HttpGet]
      [Authorize]
      public IActionResult UserMessages()
      {
	      var currentUser = _userService.GetCurrentUser();
			var messages = _contactMessageService.GetContactMessagesByUsername(currentUser.UserName);
			return View(messages);
		}

	}
}
