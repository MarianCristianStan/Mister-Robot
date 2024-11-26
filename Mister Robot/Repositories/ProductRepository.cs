using Microsoft.EntityFrameworkCore;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;

namespace Mister_Robot.Repositories
{
   public class ProductRepository : RepositoryBase<Product>, IProductRepository
   {
      private readonly MisterRobotContext _context;
      public ProductRepository(MisterRobotContext context) : base(context)
      {
         _context = context;
      }
      public IQueryable<Product> FindByCondition(System.Linq.Expressions.Expression<System.Func<Product, bool>> expression)
      {
         return _context.Set<Product>().Where(expression).AsNoTracking();
      }
   }
}
