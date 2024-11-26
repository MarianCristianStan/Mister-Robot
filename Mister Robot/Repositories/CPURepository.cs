using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class CPURepository : RepositoryBase<CPU>, ICPURepository
   {
      public CPURepository(MisterRobotContext context) : base(context)
      {
      }
   }
}
