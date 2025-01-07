using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class WishlistService : GenericServiceRepo<Wishlist>, IWishlistService
	{
		public WishlistService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.WishlistRepository, repositoryWrapper)
		{
		}

		public Wishlist GetWishlistByUserId(string userId)
		{
			return _repositoryWrapper.WishlistRepository.FindByCondition(w => w.UserId == userId).FirstOrDefault();
		}
	}
}
