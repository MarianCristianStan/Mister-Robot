using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class FeatureService : GenericServiceRepo<Feature>, IFeatureService
	{
		public FeatureService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.FeatureRepository, repositoryWrapper)
		{
		}
		public Feature GetFeatureById(string featureId)
		{
			return _repositoryWrapper.FeatureRepository.FindByCondition(f => f.FeatureId == featureId).FirstOrDefault();
		}
	}
}
