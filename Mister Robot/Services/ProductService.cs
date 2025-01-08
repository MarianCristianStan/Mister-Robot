using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class ProductService : GenericServiceRepo<Product>, IProductService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public ProductService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.ProductRepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<Product> GetProductsByCategory(string categoryId)
		{
			return _repositoryWrapper.ProductRepository.FindByCondition(p => p.ProductCategoryId == categoryId).ToList();
		}

		public IEnumerable<Product> SearchProducts(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				return _repositoryWrapper.ProductRepository.FindAll();
			}

			return _repositoryWrapper.ProductRepository
				.FindByCondition(p => p.Name.Contains(searchTerm) || (p.Description != null && p.Description.Contains(searchTerm)));
		}

      

    }
}
