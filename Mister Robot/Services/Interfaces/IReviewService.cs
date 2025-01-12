using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IReviewService : IGenericServiceRepo<Review>
	{
		IEnumerable<Review> GetReviewsByProductId(string productId);
	}
}
