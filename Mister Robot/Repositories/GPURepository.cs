using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class GPURepository : RepositoryBase<GPU>, IGPURepository
   {
      public GPURepository(MisterRobotContext context) : base(context)
      {
      }
   }
}
