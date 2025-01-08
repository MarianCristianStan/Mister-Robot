using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
    public interface IOrderProductService : IGenericServiceRepo<OrderProduct>
    {
        List<OrderProduct> GetOrderProductsByOrderId(string orderId);
        void DeleteCompositeKey(string orderId, string productId);
    }
}
