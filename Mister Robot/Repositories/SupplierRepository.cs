using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
   {
      public SupplierRepository(MisterRobotContext context) : base(context)
      {
      }
   }
}
