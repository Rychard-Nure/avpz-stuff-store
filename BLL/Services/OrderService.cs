using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            var model = await _unitOfWork.Orders.AddAsync(_mapper.Map<Order>(order));
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderModel>(model);
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(int gameId, int quantity, decimal price, int orderId)
        {
            OrderDetail orderDetail = new()
            {
                GameId = gameId,
                Quantity = quantity,
                Price = price,
                OrderId = orderId
            };
            var model = await _unitOfWork.OrderDetails.AddAsync(orderDetail);
            await _unitOfWork.SaveAsync();

            return model;
        }

        public async Task SellGame(int gameId, int quantity)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(gameId);
            game.Sold += quantity;
            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();
        }
    }
}
