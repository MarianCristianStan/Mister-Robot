using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class CartProductService : GenericServiceRepo<CartProduct>, ICartProductService
	{
		public CartProductService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.CartProductRepository, repositoryWrapper)
		{
		}

		public List<CartProduct> GetCartProductsByCartId(string cartId)
		{
			return _repositoryWrapper.CartProductRepository.FindByCondition(cp => cp.CartId == cartId).ToList();
		}
	}
}
