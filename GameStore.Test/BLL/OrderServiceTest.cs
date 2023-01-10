using BLL.Models;
using BLL.Services;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Test.BLL
{
    internal class OrderServiceTest
    {
        [Test]
        public async Task OrderService_CreateOrderAsync_ReturnsOrderModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Orders.AddAsync(It.IsAny<Order>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var newOrder = OrderModels.First();

            //actual
            await orderService.CreateOrderAsync(newOrder);

            mockUnitOfWork.Verify(x => x.Orders.AddAsync(It.Is<Order>(x =>
                    x.Comment == newOrder.Comment &&
                    x.TotalPrice == newOrder.TotalPrice &&
                    x.UserId == newOrder.UserId && 
                    x.Payment == newOrder.Payment)), Times.Once());

            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task OrderService_CreateOrderDetailAsync_ReturnsOrderDetailModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.OrderDetails.AddAsync(It.IsAny<OrderDetail>()));

            var orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var newOrder = OrderDetails.First();

            //actual
            await orderService.CreateOrderDetailAsync(newOrder.GameId, newOrder.Quantity, newOrder.Price, newOrder.OrderId);

            mockUnitOfWork.Verify(x => x.OrderDetails.AddAsync(It.Is<OrderDetail>(x =>
                    x.GameId == newOrder.GameId &&
                    x.Price == newOrder.Price &&
                    x.OrderId == newOrder.OrderId &&
                    x.Quantity == newOrder.Quantity)), Times.Once());

            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        #region TestData
        public List<OrderModel> OrderModels =>
            new List<OrderModel>
            {
                new OrderModel 
                {
                    Comment = "comment1", 
                    UserId = "id1", 
                    Payment = PaymentType.Card, 
                    TotalPrice = 100, 
                    OrderDetailIds = Array.Empty<int>(),
                },
                new OrderModel
                {
                    Comment = "comment2",
                    UserId = "id2",
                    Payment = PaymentType.Cash,
                    TotalPrice = 200,
                    OrderDetailIds = Array.Empty<int>(),
                }
            };

        public List<OrderDetail> OrderDetails =>
            new List<OrderDetail>
            {
                new OrderDetail()
                {
                    OrderId = 1,
                    GameId = 1,
                    Price = 100,
                    Quantity = 2
                },
                new OrderDetail()
                {
                    OrderId = 2,
                    GameId = 1,
                    Price = 100,
                    Quantity = 2
                }
            };
        #endregion
    }
}
