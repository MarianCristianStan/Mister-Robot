using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface ICartProductService : IGenericServiceRepo<CartProduct>
	{
		List<CartProduct> GetCartProductsByCartId(string cartId);
	}
}
