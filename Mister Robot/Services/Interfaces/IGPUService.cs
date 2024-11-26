using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IGPUService : IGenericServiceRepo<GPU>
	{
		List<GPU> GetGPUsByMemorySize(int memorySize);
	}
}
