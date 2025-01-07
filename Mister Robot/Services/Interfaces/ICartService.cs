using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface ICartService : IGenericServiceRepo<Cart>
	{
		Cart GetCartByUserId(string userId);
	}
}
