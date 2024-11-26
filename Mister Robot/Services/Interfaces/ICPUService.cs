using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface ICPUService : IGenericServiceRepo<CPU>
	{
		List<CPU> GetCPUsBySocketType(string socketType);
	}
}
