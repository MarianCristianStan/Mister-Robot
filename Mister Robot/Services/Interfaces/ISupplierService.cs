using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface ISupplierService : IGenericServiceRepo<Supplier>
	{
		Supplier GetSupplierByName(string name);
	}
}
