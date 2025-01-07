using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class CartRepository : RepositoryBase<Cart>, ICartRepository
	{
		public CartRepository(MisterRobotContext context) : base(context) { }
	}
}
