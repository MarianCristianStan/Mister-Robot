using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
	public class ContactMessageRepository : RepositoryBase<ContactMessage>, IContactMessageRepository
	{
		public ContactMessageRepository(MisterRobotContext context) : base(context) { }
	}
}
