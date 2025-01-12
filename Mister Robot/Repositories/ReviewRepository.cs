using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
	{
		public ReviewRepository(MisterRobotContext context) : base(context)
		{
		}
	}
}