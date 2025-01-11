using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IFeatureService : IGenericServiceRepo<Feature>
	{
		 Feature GetFeatureById(string featureId);
	}
}
