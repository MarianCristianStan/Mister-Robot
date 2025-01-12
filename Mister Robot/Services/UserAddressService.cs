using System.Collections.Generic;
using System.Linq;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class UserAddressService : GenericServiceRepo<UserAddress>, IUserAddressService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public UserAddressService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.UserAddressRepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<UserAddress> GetAddressesByUserId(string userId)
      {
         return _repositoryWrapper.UserAddressRepository
            .FindByCondition(ua => ua.UserId == userId)
            .ToList();
      }

      public UserAddress? GetFirstAddressByUserId(string userId)
      {
         return _repositoryWrapper.UserAddressRepository
            .FindByCondition(ua => ua.UserId == userId)
            .FirstOrDefault();
      }
   }
}
