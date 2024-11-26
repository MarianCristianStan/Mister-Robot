using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
	{
		public UserAddressRepository(MisterRobotContext context) : base(context)
		{
		}
	}
}
