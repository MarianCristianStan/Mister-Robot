using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
    public class WishlistProductService : GenericServiceRepo<WishlistProduct>, IWishlistProductService
    {
        public WishlistProductService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper.WishlistProductRepository, repositoryWrapper) { }

        public List<WishlistProduct> GetWishlistProductsByWishlistId(string wishlistId)
        {
            return _repositoryWrapper.WishlistProductRepository.FindByCondition(wp => wp.WishlistId == wishlistId).ToList();
        }

        public void DeleteCompositeKey(string wishlistId, string productId)
        {
            var entity = _repository.FindByCondition(e =>
                EF.Property<string>(e, "WishlistId") == wishlistId &&
                EF.Property<string>(e, "ProductId") == productId
            ).FirstOrDefault();

            if (entity != null)
            {
                _repository.Delete(entity);
                _repositoryWrapper.Save();
            }
        }
       

   }
}
