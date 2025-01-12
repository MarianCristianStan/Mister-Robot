using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IUserService : IGenericServiceRepo<User>
	{
		User GetCurrentUser();
		User GetByUsername(string username);
		User GetByUserId(string userId);
		Task<bool> IsUserAdminAsync(User user);
	}
}
