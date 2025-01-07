using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class ProductFeatureRepository : RepositoryBase<ProductFeature>, IProductFeatureRepository
	{
		public ProductFeatureRepository(MisterRobotContext context) : base(context) { }
	}
}
