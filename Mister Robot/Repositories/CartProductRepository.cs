using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class CartProductRepository : RepositoryBase<CartProduct>, ICartProductRepository
	{
		public CartProductRepository(MisterRobotContext context) : base(context)
		{
		}
	}
}
