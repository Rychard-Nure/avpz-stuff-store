using BLL.Models;
using DataLayer.Entities;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrderAsync(OrderModel order);
        Task<OrderDetail> CreateOrderDetailAsync(int gameId, int quantity, decimal price, int orderId);
        Task SellGame(int gameId, int quantity);
    }
}
