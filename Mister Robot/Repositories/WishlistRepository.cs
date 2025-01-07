using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class WishlistRepository : RepositoryBase<Wishlist>, IWishlistRepository
	{
		public WishlistRepository(MisterRobotContext context) : base(context) { }
	}
}
