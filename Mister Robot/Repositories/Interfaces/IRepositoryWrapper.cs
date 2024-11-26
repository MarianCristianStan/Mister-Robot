using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories.Interfaces
{
	public interface IRepositoryWrapper
	{
		IUserRepository UserRepository { get; }
		IUserAddressRepository UserAddressRepository { get; }
		IProductRepository ProductRepository { get; }
		ICPURepository CPURepository { get; }
		IGPURepository GPURepository { get; }
		ISupplierRepository SupplierRepository { get; }
		IProductCategoryRepository ProductCategoryRepository { get; }
		void Save();
	}
}
