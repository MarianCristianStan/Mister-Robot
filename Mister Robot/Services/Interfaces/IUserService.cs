using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IUserService : IGenericServiceRepo<User>
	{
		User GetCurrentUser();
		User GetByUsername(string username);
		Task<bool> IsUserAdminAsync(User user);
	}
}
