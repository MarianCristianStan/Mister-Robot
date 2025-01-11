using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IProductFeatureService : IGenericServiceRepo<ProductFeature>
	{
		List<ProductFeature> GetFeaturesByProductId(string productId);
		void DeleteCompositeKey(string productId, string featureId);
		public ProductFeature GetFeatureById(string productId, string featureId);
	}
}
