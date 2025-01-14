﻿using Mister_Robot.Models;

namespace Mister_Robot.Services.Interfaces
{
	public interface IOrderService : IGenericServiceRepo<Order>
	{
		List<Order> GetOrdersByUserId(string userId);
      Order CreateOrder(Order order);
      Order GetOrderById(string orderId);
   }	
}
