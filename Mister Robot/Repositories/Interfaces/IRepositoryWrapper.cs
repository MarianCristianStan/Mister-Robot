﻿using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		IUserRepository UserRepository { get; }
		IUserAddressRepository UserAddressRepository { get; }
		IProductRepository ProductRepository { get; }
		ISupplierRepository SupplierRepository { get; }
		IProductCategoryRepository ProductCategoryRepository { get; }
		IFeatureRepository FeatureRepository { get; }
		IProductFeatureRepository ProductFeatureRepository { get; }
		IOrderRepository OrderRepository { get; }
		IWishlistRepository WishlistRepository { get; }
		ICartRepository CartRepository { get; }
		ICartProductRepository CartProductRepository { get; }
		IWishlistProductRepository WishlistProductRepository { get; }
		IOrderProductRepository OrderProductRepository { get; }
		IReviewRepository ReviewRepository { get; }
		IContactMessageRepository ContactMessageRepository {get; }

		void Save();
	}
}
