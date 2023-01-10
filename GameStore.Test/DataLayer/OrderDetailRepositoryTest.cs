using DataLayer.Data;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Test.DataLayer
{
    internal class OrderDetailRepositoryTest
    {
        [Test]
        public async Task OrderDetailRepository_GetAllAsync_ReturnAllValues()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderDetailRepository = new OrderDetailRepository(context);
            var orderDetails = await orderDetailRepository.GetAllAsync();

            Assert.That(orderDetails, Is.EqualTo(UnitTestHelper.orderDetails).Using(new OrderDetailEqualityComparer()));
        }

        [TestCase(1)]
        public async Task OrderDetailRepository_GetByIdAsync_ReturnSingleOrderDetail(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var OrderDetailRepository = new OrderDetailRepository(context);
            var OrderDetail = await OrderDetailRepository.GetByIdAsync(id);
            var expectedOrderDetail = UnitTestHelper.orderDetails.FirstOrDefault(i => i.Id == id);

            Assert.That(OrderDetail, Is.EqualTo(expectedOrderDetail).Using(new OrderDetailEqualityComparer()));
        }

        [Test]
        public async Task OrderDetailRepository_AddAsync_AddOrderDetailToDatabase()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var OrderDetailRepository = new OrderDetailRepository(context);
            var OrderDetail = await OrderDetailRepository.GetByIdAsync(1);
            OrderDetail.Id = 3;
            await OrderDetailRepository.AddAsync(OrderDetail);
            await context.SaveChangesAsync();

            Assert.That(context.OrderDetails.Count(), Is.EqualTo(3));
        }

        [TestCase(1)]
        public async Task OrderDetailRepository_DeleteByIdAsync_DeleteOrderDetail(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var OrderDetailRepository = new OrderDetailRepository(context);
            await OrderDetailRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            Assert.That(context.OrderDetails.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task OrderDetailRepository_DeleteAsync_DeleteOrderDetail()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var OrderDetailRepository = new OrderDetailRepository(context);
            var OrderDetail = await OrderDetailRepository.GetByIdAsync(1);
            OrderDetailRepository.Delete(OrderDetail);
            await context.SaveChangesAsync();

            Assert.That(context.OrderDetails.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GOrderDetailRepository_Update_UpdateOrderDetail()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var OrderDetailRepository = new OrderDetailRepository(context);
            var OrderDetail = await OrderDetailRepository.GetByIdAsync(1);
            OrderDetail.Price = 100;
            var result = OrderDetailRepository.Update(OrderDetail);
            await context.SaveChangesAsync();

            Assert.That(result, Is.EqualTo(OrderDetail));
        }
    }
}
