using System.Linq;
using Mister_Robot.Models;
using Mister_Robot.Repositories;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class SupplierService : GenericServiceRepo<Supplier>, ISupplierService
	{
		public SupplierService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.SupplierRepository, repositoryWrapper)
		{
		}

		public Supplier GetSupplierByName(string name)
		{
			return _repositoryWrapper.SupplierRepository
				 .FindByCondition(s => s.Name.ToLower() == name.ToLower())
				 .FirstOrDefault();
		}
	}
}
