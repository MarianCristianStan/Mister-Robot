using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IProductCategoryService : IGenericServiceRepo<ProductCategory>
	{
		ProductCategory GetCategoryByName(string name);
	}
}
