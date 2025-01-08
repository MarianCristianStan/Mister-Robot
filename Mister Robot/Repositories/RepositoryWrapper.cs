using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Repositories
{
   public class RepositoryWrapper : IRepositoryWrapper
   {
      private MisterRobotContext _context;

		private IUserRepository? _userRepository;
		private IProductRepository? _productRepository;
		private IProductCategoryRepository? _productCategoryRepository;
		private ISupplierRepository? _supplierRepository;
		private IUserAddressRepository? _userAddressRepository;
		private ICartRepository? _cartRepository;
		private ICartProductRepository? _cartProductRepository;
		private IWishlistRepository? _wishlistRepository;
		private IOrderRepository? _orderRepository;
		private IProductFeatureRepository? _productFeatureRepository;
        private IWishlistProductRepository? _wishlistProductRepository;
        private IOrderProductRepository? _orderProductRepository;
        public RepositoryWrapper(MisterRobotContext context)
      {
         _context = context;
      }

      public IUserRepository UserRepository
      {
         get
         {
            if (_userRepository == null)
            {
               _userRepository = new UserRepository(_context);
            }
            return _userRepository;
         }
      }

      public IProductRepository ProductRepository
      {
         get
         {
            if (_productRepository == null)
            {
               _productRepository = new ProductRepository(_context);
            }
            return _productRepository;
         }
      }

      public IProductCategoryRepository ProductCategoryRepository
      {
         get
         {
            if (_productCategoryRepository == null)
            {
               _productCategoryRepository = new ProductCategoryRepository(_context);
            }
            return _productCategoryRepository;
         }
      }

      public ISupplierRepository SupplierRepository
      {
         get
         {
            if (_supplierRepository == null)
            {
               _supplierRepository = new SupplierRepository(_context);
            }
            return _supplierRepository;
         }
      }

		public IUserAddressRepository UserAddressRepository
		{
			get
			{
				if (_userAddressRepository == null)
				{
					_userAddressRepository = new UserAddressRepository(_context);
				}
				return _userAddressRepository;
			}
		}
		public ICartRepository CartRepository
		{
			get
			{
				if (_cartRepository == null)
				{
					_cartRepository = new CartRepository(_context);
				}
				return _cartRepository;
			}
		}

		public ICartProductRepository CartProductRepository
		{
			get
			{
				if (_cartProductRepository == null)
				{
					_cartProductRepository = new CartProductRepository(_context);
				}
				return _cartProductRepository;
			}
		}
        public IWishlistProductRepository WishlistProductRepository
        {
            get
            {
                if (_wishlistProductRepository == null)
                {
                    _wishlistProductRepository = new WishlistProductRepository(_context);
                }
                return _wishlistProductRepository;
            }
        }

        public IOrderProductRepository OrderProductRepository
        {
            get
            {
                if (_orderProductRepository == null)
                {
                    _orderProductRepository = new OrderProductRepository(_context);
                }
                return _orderProductRepository;
            }
        }
        public IWishlistRepository WishlistRepository
		{
			get
			{
				if (_wishlistRepository == null)
				{
					_wishlistRepository = new WishlistRepository(_context);
				}
				return _wishlistRepository;
			}
		}

		public IOrderRepository OrderRepository
		{
			get
			{
				if (_orderRepository == null)
				{
					_orderRepository = new OrderRepository(_context);
				}
				return _orderRepository;
			}
		}

		public IProductFeatureRepository ProductFeatureRepository
		{
			get
			{
				if (_productFeatureRepository == null)
				{
					_productFeatureRepository = new ProductFeatureRepository(_context);
				}
				return _productFeatureRepository;
			}
		}
		public void Save()
      {
         _context.SaveChanges();
      }
   }
}
