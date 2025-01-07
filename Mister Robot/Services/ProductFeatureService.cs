using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class ProductFeatureService : GenericServiceRepo<ProductFeature>, IProductFeatureService
	{
		public ProductFeatureService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.ProductFeatureRepository, repositoryWrapper)
		{
		}

		public List<ProductFeature> GetFeaturesByProductId(string productId)
		{
			return _repositoryWrapper.ProductFeatureRepository.FindByCondition(f => f.ProductId == productId).ToList();
		}
	}
}
