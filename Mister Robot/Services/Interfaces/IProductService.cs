using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IProductService : IGenericServiceRepo<Product>
	{
		List<Product> GetProductsByCategory(int categoryId);
      IEnumerable<Product> SearchProducts(string searchTerm);
   }
}
