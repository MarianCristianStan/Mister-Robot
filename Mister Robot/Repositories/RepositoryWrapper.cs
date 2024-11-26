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
      private ICPURepository? _cpuRepository;
      private IGPURepository? _gpuRepository;
      private IUserAddressRepository? _userAddressRepository;

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

      public ICPURepository CPURepository
      {
         get
         {
            if (_cpuRepository == null)
            {
               _cpuRepository = new CPURepository(_context);
            }
            return _cpuRepository;
         }
      }

      public IGPURepository GPURepository
      {
         get
         {
            if (_gpuRepository == null)
            {
               _gpuRepository = new GPURepository(_context);
            }
            return _gpuRepository;
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

		

		public void Save()
      {
         _context.SaveChanges();
      }
   }
}
