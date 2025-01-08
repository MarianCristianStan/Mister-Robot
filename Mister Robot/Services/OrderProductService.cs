using Microsoft.EntityFrameworkCore;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
    public class OrderProductService : GenericServiceRepo<OrderProduct>, IOrderProductService
    {
        public OrderProductService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper.OrderProductRepository, repositoryWrapper) { }

        public List<OrderProduct> GetOrderProductsByOrderId(string orderId)
        {
            return _repositoryWrapper.OrderProductRepository.FindByCondition(op => op.OrderId == orderId).ToList();
        }

        public void DeleteCompositeKey(string orderId, string productId)
        {
            var entity = _repository.FindByCondition(e =>
                EF.Property<string>(e, "OrderId") == orderId &&
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
