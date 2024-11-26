using Mister_Robot.Models;

namespace Mister_Robot.Repositories.Interfaces
{
   public interface IProductRepository : IRepositoryBase<Product>
   {
      IQueryable<Product> FindByCondition(System.Linq.Expressions.Expression<System.Func<Product, bool>> expression);
   }
}
