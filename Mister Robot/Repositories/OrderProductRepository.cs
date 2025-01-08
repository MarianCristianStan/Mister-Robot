using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
    public class OrderProductRepository : RepositoryBase<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(MisterRobotContext context) : base(context)
        {
        }
    }
}
