using Mister_Robot.Models;
using System.Collections.Generic;

namespace Mister_Robot.Services.Interfaces
{
	public interface IUserAddressService : IGenericServiceRepo<UserAddress>
	{
		List<UserAddress> GetAddressesByUserId(string userId);
       UserAddress? GetFirstAddressByUserId(string userId);

   }
}
