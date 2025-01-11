using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
	{
		public FeatureRepository(MisterRobotContext context) : base(context) { }
	}
}
