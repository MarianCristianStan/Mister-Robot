using Microsoft.AspNetCore.Identity;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class UserService : GenericServiceRepo<User>, IUserService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;
		public UserService(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
			 : base(repositoryWrapper.UserRepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public User GetByUsername(string username)
		{	
			return _repositoryWrapper.UserRepository.FindByCondition(u => u.UserName == username).FirstOrDefault();
		}

		public User GetCurrentUser()
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			if (string.IsNullOrEmpty(userId))
			{
				return null;
			}

			return (User)_userManager.FindByIdAsync(userId).Result;
		}
		public User GetByUserId(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return null;
			}

			return _repositoryWrapper.UserRepository.FindByCondition(u => u.Id == userId).FirstOrDefault();
		}

		public async Task<bool> IsUserAdminAsync(User user)
		{
			if (user == null) return false;
			var roles = await _userManager.GetRolesAsync(user);
			return roles.Contains("Admin");
		}
	}
}
