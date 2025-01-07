using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class OrderService : GenericServiceRepo<Order>, IOrderService
	{
		public OrderService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.OrderRepository, repositoryWrapper)
		{
		}

		public List<Order> GetOrdersByUserId(string userId)
		{
			return _repositoryWrapper.OrderRepository.FindByCondition(o => o.UserId == userId).ToList();
		}
	}
}
