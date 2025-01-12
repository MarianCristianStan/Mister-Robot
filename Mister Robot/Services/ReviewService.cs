using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class ReviewService : GenericServiceRepo<Review>, IReviewService
	{
		public ReviewService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.ReviewRepository, repositoryWrapper)
		{
		}

		public IEnumerable<Review> GetReviewsByProductId(string productId)
		{
			return _repositoryWrapper.ReviewRepository
				.FindByCondition(r => r.ProductId == productId);
		}
	}
}