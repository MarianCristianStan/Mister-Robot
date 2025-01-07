using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IWishlistService : IGenericServiceRepo<Wishlist>
	{
		Wishlist GetWishlistByUserId(string userId);
	}

}
