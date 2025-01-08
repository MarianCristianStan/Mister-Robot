using Mister_Robot.Areas.Identity.Pages.Account;
using Mister_Robot.Models;
using Mister_Robot.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mister_Robot.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserStore<User> _userStore;
		private readonly IUserEmailStore<User> _emailStore;
		private readonly SignInManager<User> _signInManager;
		private readonly ICartService _cartService;
		private readonly IWishlistService _wishlistService;
		public AuthService(UserManager<User> userManager, IUserStore<User> userStore, SignInManager<User> signInManager, ICartService cartService,
			IWishlistService wishlistService)
		{
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_signInManager = signInManager;
			_cartService = cartService;
			_wishlistService = wishlistService;
		}

      public async Task<IdentityResult> RegisterUserAsync(RegisterModel.InputModel inputModel)
      {
         var user = new User
         {
            UserName = inputModel.Username,
            Email = inputModel.Email,
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName
         };

         await _userStore.SetUserNameAsync(user, inputModel.Username, CancellationToken.None);
         await _emailStore.SetEmailAsync(user, inputModel.Email, CancellationToken.None);

         var defaultProfilePicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "default_user.png");
         user.ProfilePicture = await File.ReadAllBytesAsync(defaultProfilePicturePath);

			var result = await _userManager.CreateAsync(user, inputModel.Password);

			if (result.Succeeded)
			{
				var cart = new Cart
				{
					UserId = user.Id
				};
				_cartService.Add(cart);

				var wishlist = new Wishlist
				{
					UserId = user.Id
				};
				_wishlistService.Add(wishlist);

				user.Cart = cart;
				user.Wishlist = wishlist;
				await _userManager.UpdateAsync(user); 
			}

			return result;
      }

      public async Task<IActionResult> HandleUserRegistrationAsync(string email, string returnUrl)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (_userManager.Options.SignIn.RequireConfirmedAccount)
			{
				return new RedirectToPageResult("RegisterConfirmation", new { email, returnUrl });
			}
			else
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
				return new LocalRedirectResult(returnUrl);
			}
		}
		public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
		{
			return await _signInManager.GetExternalAuthenticationSchemesAsync();
		}
		

		public async Task<IActionResult> HandleUserLoginAsync(LoginModel.InputModel inputModel, string returnUrl)
		{
			var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return new LocalRedirectResult(returnUrl);
			}
			if (result.RequiresTwoFactor)
			{
				return new RedirectToPageResult("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = inputModel.RememberMe });
			}
			if (result.IsLockedOut)
			{
				return new RedirectToPageResult("./Lockout");
			}

			return null;
		}

		private IUserEmailStore<User> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<User>)_userStore;
		}

		private User CreateUser()
		{
			try
			{
				return Activator.CreateInstance<User>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
				                                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
				                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}
	}
}