using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
   {
      public ProductCategoryRepository(MisterRobotContext context) : base(context)
      {
      }
   }
}
