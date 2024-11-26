// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mister_Robot.Areas.Identity.Pages.Account
{
   public class RegisterModel : PageModel
   {
      private readonly SignInManager<User> _signInManager;
      private readonly UserManager<User> _userManager;
      private readonly IUserStore<User> _userStore;
      private readonly IUserEmailStore<User> _emailStore;
      private readonly ILogger<RegisterModel> _logger;
      private readonly IAuthService _authService;

      public RegisterModel(
         UserManager<User> userManager,
         IUserStore<User> userStore,
         SignInManager<User> signInManager,
         ILogger<RegisterModel> logger,
         IAuthService authService)
      {
         _userManager = userManager;
         _userStore = userStore;
         _emailStore = GetEmailStore();
         _signInManager = signInManager;
         _logger = logger;
         _authService = authService;
      }


      [BindProperty]
      public InputModel Input { get; set; }


      public string ReturnUrl { get; set; }


      public IList<AuthenticationScheme> ExternalLogins { get; set; }


      public class InputModel
      {
         [Required(ErrorMessage = "First name is required.")]
         [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
         public string FirstName { get; set; }

         [Required(ErrorMessage = "Last name is required.")]
         [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
         public string LastName { get; set; }

         [Required(ErrorMessage = "Email is required.")]
         [EmailAddress(ErrorMessage = "Invalid email address.")]
         [Display(Name = "Email")]
         public string Email { get; set; }

         [Required(ErrorMessage = "Username is required.")]
         [Display(Name = "Username")]
         public string Username { get; set; }

         [Required(ErrorMessage = "Password is required.")]
         [StringLength(100, ErrorMessage = "The password must be at least {2} and at most {1} characters long.", MinimumLength = 8)]
         [DataType(DataType.Password)]
         [Display(Name = "Password")]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm Password")]
         [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
         public string ConfirmPassword { get; set; }
      }



      public async Task OnGetAsync(string returnUrl = null)
      {
         ReturnUrl = returnUrl;
         ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      }

      public async Task<IActionResult> OnPostAsync(string returnUrl = null)
      {
         returnUrl ??= Url.Content("~/");
         ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
         if (ModelState.IsValid)
         {
            var result = await _authService.RegisterUserAsync(Input);

            if (result.Succeeded)
            {

               var user = await _userManager.FindByEmailAsync(Input.Email);
               await _userManager.AddToRoleAsync(user, Enums.Roles.Basic.ToString());
               TempData["SuccessMessage"] = "You have successfully create an account.";
               TempData["ReturnUrl"] = returnUrl;
               await _authService.HandleUserRegistrationAsync(Input.Email, returnUrl);
               return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
               ModelState.AddModelError(string.Empty, error.Description);
            }
         }

         // If we got this far, something failed, redisplay form
         return Page();
      }


      private IUserEmailStore<User> GetEmailStore()
      {
         if (!_userManager.SupportsUserEmail)
         {
            throw new NotSupportedException("The default UI requires a user store with email support.");
         }
         return (IUserEmailStore<User>)_userStore;
      }
   }
}
