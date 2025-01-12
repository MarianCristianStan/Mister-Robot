using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class ContactMessageService : GenericServiceRepo<ContactMessage>, IContactMessageService
	{
		public ContactMessageService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.ContactMessageRepository, repositoryWrapper)
		{
		}


		public ContactMessage? GetContactMessageById(string contactMessageId)
		{
			return _repositoryWrapper.ContactMessageRepository
				.FindByCondition(cm => cm.ContactMessageId == contactMessageId)
				.FirstOrDefault();
		}
		public List<ContactMessage> GetContactMessagesByUsername(string username)
		{
			return _repositoryWrapper.ContactMessageRepository
				.FindByCondition(cm => cm.Username == username)
				.ToList();
		}
		public List<ContactMessage> GetContactMessagesByReplyStatus(bool isReplied)
		{
			return _repositoryWrapper.ContactMessageRepository
				.FindByCondition(cm => cm.IsReplied == isReplied)
				.ToList();
		}

	}
}
