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
    public class LoginModel : PageModel
    {

		private readonly IAuthService _authService;
		private readonly SignInManager<User> _signInManager;
		private readonly ILogger<LoginModel> _logger;

		public LoginModel(IAuthService authService, SignInManager<User> signInManager, ILogger<LoginModel> logger)
		{
			_authService = authService;
			_signInManager = signInManager;
			_logger = logger;
		}

		[BindProperty]
        public InputModel Input { get; set; }

      
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        
        public string ReturnUrl { get; set; }

        
        [TempData]
        public string ErrorMessage { get; set; }

       
        public class InputModel
        {

	        [Required(ErrorMessage = "Username is required.")]
	        [Display(Name = "Username")]
	        public string Username { get; set; }

	        [Required(ErrorMessage = "Password is required.")]
	        [DataType(DataType.Password)]
	        [Display(Name = "Password")]
	        public string Password { get; set; }


			 [Display(Name = "Remember me?")]
			 public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
	        returnUrl ??= Url.Content("~/");

	        ExternalLogins = (await _authService.GetExternalAuthenticationSchemesAsync()).ToList();

	        if (ModelState.IsValid)
	        {
		        var result = await _authService.HandleUserLoginAsync(Input, returnUrl);

				if (result != null)
				{
					TempData["SuccessMessage"] = "You have successfully logged in.";
					TempData["ReturnUrl"] = returnUrl;
					return RedirectToPage(); // Stay on the same page to show the pop-up
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

	        // If we got this far, something failed, redisplay form
	        return Page();
        }
	}
}
