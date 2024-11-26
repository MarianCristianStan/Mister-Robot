using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class UserRepository : RepositoryBase<User>, IUserRepository
   {
      public UserRepository(MisterRobotContext context) : base(context)
      {
      }
   }
}
