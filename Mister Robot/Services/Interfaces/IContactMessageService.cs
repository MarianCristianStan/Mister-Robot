using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IContactMessageService : IGenericServiceRepo<ContactMessage>
	{

		ContactMessage? GetContactMessageById(string contactMessageId);

		 List<ContactMessage> GetContactMessagesByUsername(string username);
		
		 List<ContactMessage> GetContactMessagesByReplyStatus(bool isReplied);
	}
}
