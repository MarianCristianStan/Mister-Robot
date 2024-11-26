using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class ProductCategoryService : GenericServiceRepo<ProductCategory>, IProductCategoryService
	{
		public ProductCategoryService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.ProductCategoryRepository, repositoryWrapper)
		{
		}

		public ProductCategory GetCategoryByName(string name)
		{
			return _repositoryWrapper.ProductCategoryRepository
				 .FindByCondition(c => c.Name.ToLower() == name.ToLower())
				 .FirstOrDefault();
		}
	}
}
