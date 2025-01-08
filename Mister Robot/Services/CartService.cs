using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
   public class CartService : GenericServiceRepo<Cart>, ICartService
   {
      private readonly IRepositoryWrapper _repositoryWrapper;
      private readonly IProductService _productService;
      private readonly ICartProductService _cartProductService;
      private readonly IUserService _userService;

      public CartService(IRepositoryWrapper repositoryWrapper, IProductService productService, ICartProductService cartProductService, IUserService userService)
          : base(repositoryWrapper.CartRepository, repositoryWrapper)
      {
         _productService = productService;
         _repositoryWrapper = repositoryWrapper;
         _cartProductService = cartProductService;
         _userService = userService;
      }

      public Cart GetCartByUserId(string userId)
      {
         return GetAll().FirstOrDefault(c => c.UserId == userId);
      }


      public void AddToCart(string productId)
      {
         var product = _productService.GetById(productId);
         var user = _userService.GetCurrentUser();

         if (product == null) throw new Exception("Product not found.");

         var cart = GetCartByUserId(user.Id);
         if (cart == null)
         {
            cart = new Cart { UserId = user.Id };
            Add(cart);
         }

         // Check if the product already exists in the cart
         var cartProduct = _cartProductService.GetCartProductsByCartId(cart.CartId)
            .FirstOrDefault(cp => cp.ProductId == productId);

         if (cartProduct != null)
         {
            cartProduct.Quantity += 1;
            _cartProductService.Update(cartProduct);
         }
         else
         {
            var newCartProduct = new CartProduct
            {
               CartId = cart.CartId,
               ProductId = productId,
               Quantity = 1
            };
            _cartProductService.Add(newCartProduct);
         }
      }


      public IEnumerable<CartProduct> GetCartItems()
      {
         var user = _userService.GetCurrentUser();
         var cart = GetCartByUserId(user.Id);
         return cart != null ? _cartProductService.GetCartProductsByCartId(cart.CartId) : new List<CartProduct>();
      }

      public void RemoveFromCart(string productId)
      {
	      var user = _userService.GetCurrentUser();
	      var cart = _repositoryWrapper.CartRepository.FindByCondition(c => c.UserId == user.Id).FirstOrDefault();

	      if (cart == null)
		      return;

	      var cartProduct = _repositoryWrapper.CartProductRepository
		      .FindByCondition(cp => cp.CartId == cart.CartId && cp.ProductId == productId)
		      .FirstOrDefault();

	      if (cartProduct != null)
	      {
		      _repositoryWrapper.CartProductRepository.Delete(cartProduct);
		      _repositoryWrapper.Save();
	      }
      }

      public void UpdateCartQuantity(string productId, int quantity)
      {
	      var user = _userService.GetCurrentUser();
	      var cart = _repositoryWrapper.CartRepository.FindByCondition(c => c.UserId == user.Id).FirstOrDefault();

	      if (cart == null)
		      return;

	      var cartProduct = _repositoryWrapper.CartProductRepository
		      .FindByCondition(cp => cp.CartId == cart.CartId && cp.ProductId == productId)
		      .FirstOrDefault();

	      if (cartProduct != null)
	      {
		      cartProduct.Quantity = quantity;
		      _repositoryWrapper.CartProductRepository.Update(cartProduct);
		      _repositoryWrapper.Save();
	      }
      }

      public void ClearCart()
      {
	      var user = _userService.GetCurrentUser();
	      var cart = GetCartByUserId(user.Id);

	      if (cart != null)
	      {
		      var cartProducts = _cartProductService.GetCartProductsByCartId(cart.CartId);
		      foreach (var product in cartProducts)
		      {
			      _cartProductService.DeleteCompositeKey(product.CartId, product.ProductId);
		      }
	      }
      }

	}
}
