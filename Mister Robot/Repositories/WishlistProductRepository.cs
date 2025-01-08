using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
    public class WishlistProductRepository : RepositoryBase<WishlistProduct>, IWishlistProductRepository
    {
        public WishlistProductRepository(MisterRobotContext context) : base(context)
        {
        }
    }
}
