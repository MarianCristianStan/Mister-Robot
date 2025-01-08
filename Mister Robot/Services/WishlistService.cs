using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
   public class WishlistService : GenericServiceRepo<Wishlist>, IWishlistService
   {
      private readonly IRepositoryWrapper _repositoryWrapper;
      private readonly IProductService _productService;
      private readonly IUserService _userService;
      private readonly IWishlistProductService _wishlistProductService;

      public WishlistService(IRepositoryWrapper repositoryWrapper, IProductService productService, IUserService userService, IWishlistProductService wishlistProductService)
          : base(repositoryWrapper.WishlistRepository, repositoryWrapper)
      {
         _repositoryWrapper = repositoryWrapper;
         _productService = productService;
         _userService = userService;
         _wishlistProductService = wishlistProductService;
      }

      public Wishlist GetWishlistByUserId(string userId)
      {
         return GetAll().FirstOrDefault(w => w.UserId == userId);
      }

      public void AddToWishlist(string productId)
      {
         var product = _productService.GetById(productId);
         var user = _userService.GetCurrentUser();

         if (product == null) throw new Exception("Product not found.");

         var wishlist = GetWishlistByUserId(user.Id);
         if (wishlist == null)
         {
            wishlist = new Wishlist { UserId = user.Id };
            Add(wishlist);
         }

         var wishlistProduct = _repositoryWrapper.WishlistProductRepository
             .FindByCondition(wp => wp.WishlistId == wishlist.WishlistId && wp.ProductId == productId)
             .FirstOrDefault();

         if (wishlistProduct == null)
         {
            var newWishlistProduct = new WishlistProduct
            {
               WishlistId = wishlist.WishlistId,
               ProductId = productId
            };
            _wishlistProductService.Add(newWishlistProduct);
         }
      }

      public IEnumerable<WishlistProduct> GetWishlistItems()
      {
         var user = _userService.GetCurrentUser();
         var wishlist = GetWishlistByUserId(user.Id);
         return wishlist != null ? _repositoryWrapper.WishlistProductRepository.FindByCondition(wp => wp.WishlistId == wishlist.WishlistId).ToList() : new List<WishlistProduct>();
      }

      public void RemoveFromWishlist(string productId)
      {
         var user = _userService.GetCurrentUser();
         var wishlist = GetWishlistByUserId(user.Id);

         if (wishlist == null)
            return;

         var wishlistProduct = _repositoryWrapper.WishlistProductRepository
             .FindByCondition(wp => wp.WishlistId == wishlist.WishlistId && wp.ProductId == productId)
             .FirstOrDefault();

         if (wishlistProduct != null)
         {
            _repositoryWrapper.WishlistProductRepository.Delete(wishlistProduct);
            _repositoryWrapper.Save();
         }
      }

      public void ClearWishlist()
      {
         var user = _userService.GetCurrentUser();
         var wishlist = GetWishlistByUserId(user.Id);

         if (wishlist != null)
         {
            var wishlistProducts = _repositoryWrapper.WishlistProductRepository.FindByCondition(wp => wp.WishlistId == wishlist.WishlistId).ToList();
            foreach (var product in wishlistProducts)
            {
               _repositoryWrapper.WishlistProductRepository.Delete(product);
            }
            _repositoryWrapper.Save();
         }
      }
   }
}
