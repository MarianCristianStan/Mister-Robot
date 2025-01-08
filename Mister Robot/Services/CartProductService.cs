using Microsoft.EntityFrameworkCore;
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

		public void DeleteCompositeKey(string cartId, string productId)
		{
			var entity = _repository.FindByCondition(e =>
				EF.Property<string>(e, "CartId") == cartId &&
				EF.Property<string>(e, "ProductId") == productId
			).FirstOrDefault();

			if (entity != null)
			{
				_repository.Delete(entity);
				_repositoryWrapper.Save();
			}
			else
			{
				throw new ArgumentException($"Entity with CartId {cartId} and ProductId {productId} not found.");
			}
		}
	}
}
