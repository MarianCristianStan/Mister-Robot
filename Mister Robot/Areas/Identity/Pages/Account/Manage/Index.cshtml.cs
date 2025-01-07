using System.ComponentModel.DataAnnotations;
using Mister_Robot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mister_Robot.Areas.Identity.Pages.Account.Manage
{
	/*[Authorize(Roles = "Admin")]*/
	public class IndexModel : PageModel
   {
      private readonly UserManager<User> _userManager;
      private readonly SignInManager<User> _signInManager;

      public IndexModel(
          UserManager<User> userManager,
          SignInManager<User> signInManager)
      {
         _userManager = userManager;
         _signInManager = signInManager;
      }

      public string Username { get; set; }

      [TempData]
      public string StatusMessage { get; set; }

      [BindProperty]
      public InputModel Input { get; set; }

      public class InputModel
      {
         [Display(Name = "First Name")]
         public string FirstName { get; set; }

         [Display(Name = "Last Name")]
         public string LastName { get; set; }

         [Display(Name = "Gender")]
         public string Gender { get; set; }

         [EmailAddress]
         [Display(Name = "Email")]
         public string Email { get; set; }

         [Display(Name = "Profile Picture")]
         public byte[]? ProfilePicture { get; set; }
      }

      private async Task LoadAsync(User user)
      {
         Username = await _userManager.GetUserNameAsync(user);

         Input = new InputModel
         {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ProfilePicture = user.ProfilePicture
         };
      }

      public async Task<IActionResult> OnGetAsync()
      {
         var user = await _userManager.GetUserAsync(User);
         if (user == null)
         {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
         }

         await LoadAsync(user);
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         var user = await _userManager.GetUserAsync(User);
         if (user == null)
         {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
         }

         if (!ModelState.IsValid)
         {
            await LoadAsync(user);
            return Page();
         }

         var email = await _userManager.GetEmailAsync(user);
         if (Input.Email != email)
         {
            var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
            if (!setEmailResult.Succeeded)
            {
               StatusMessage = "Unexpected error when trying to set email.";
               return RedirectToPage();
            }
         }


         if (Request.Form.Files.Count > 0)
         {
            IFormFile file = Request.Form.Files.FirstOrDefault();
            using (var dataStream = new MemoryStream())
            {
               await file.CopyToAsync(dataStream);
               user.ProfilePicture = dataStream.ToArray();
            }
         }
         
         user.FirstName = Input.FirstName;
         user.LastName = Input.LastName;
         
         var updateResult = await _userManager.UpdateAsync(user);
         if (!updateResult.Succeeded)
         {
            StatusMessage = "Unexpected error when trying to update profile.";
            return RedirectToPage();
         }

         await _signInManager.RefreshSignInAsync(user);
         StatusMessage = "Your profile has been updated";
         return RedirectToPage();
      }
   }
}
