using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class GPUService : GenericServiceRepo<GPU>, IGPUService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public GPUService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.GPURepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<GPU> GetGPUsByMemorySize(int memorySize)
		{
			return _repositoryWrapper.GPURepository.FindByCondition(g => g.MemorySize >= memorySize).ToList();
		}
	}
}
