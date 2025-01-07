using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class CartService : GenericServiceRepo<Cart>, ICartService
	{
		public CartService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.CartRepository, repositoryWrapper)
		{
		}

		public Cart GetCartByUserId(string userId)
		{
			return _repositoryWrapper.CartRepository.FindByCondition(c => c.UserId == userId).FirstOrDefault();
		}
	}
}
