using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
    public interface IWishlistProductService : IGenericServiceRepo<WishlistProduct>
    {
        List<WishlistProduct> GetWishlistProductsByWishlistId(string wishlistId);
        void DeleteCompositeKey(string wishlistId, string productId);

   }
}
