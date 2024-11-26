using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class CPUService : GenericServiceRepo<CPU>, ICPUService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public CPUService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.CPURepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<CPU> GetCPUsBySocketType(string socketType)
		{
			return _repositoryWrapper.CPURepository.FindByCondition(c => c.SocketType == socketType).ToList();
		}
	}
}
