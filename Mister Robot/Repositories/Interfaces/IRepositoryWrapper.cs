using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		IUserRepository UserRepository { get; }
		IUserAddressRepository UserAddressRepository { get; }
		IProductRepository ProductRepository { get; }
		ISupplierRepository SupplierRepository { get; }
		IProductCategoryRepository ProductCategoryRepository { get; }
		IProductFeatureRepository ProductFeatureRepository { get; }
		IOrderRepository OrderRepository { get; }
		IWishlistRepository WishlistRepository { get; }
		ICartRepository CartRepository { get; }
		ICartProductRepository CartProductRepository { get; }
		void Save();
	}
}
