using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IProductFeatureService : IGenericServiceRepo<ProductFeature>
	{
		List<ProductFeature> GetFeaturesByProductId(string productId);
	}
}
