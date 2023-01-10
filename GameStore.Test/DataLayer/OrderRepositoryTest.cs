using DataLayer.Data;
using DataLayer.Repositories;

namespace GameStore.Test.DataLayer
{
    internal class OrderRepositoryTest
    {
        [Test]
        public async Task OrderRepository_GetAllAsync_ReturnAllValues()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var orders = await orderRepository.GetAllAsync();

            Assert.That(orders, Is.EqualTo(UnitTestHelper.orders).Using(new OrderEqualityComparer()));
        }

        [TestCase(1)]
        public async Task OrderRepository_GetByIdAsync_ReturnSingleOrder(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = await orderRepository.GetByIdAsync(id);
            var expectedOrder = UnitTestHelper.orders.FirstOrDefault(i => i.Id == id);

            Assert.That(order, Is.EqualTo(expectedOrder).Using(new OrderEqualityComparer()));
        }

        [Test]
        public async Task OrderRepository_AddAsync_AddOrderToDatabase()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = await orderRepository.GetByIdAsync(1);
            order.Id = 2;
            await orderRepository.AddAsync(order);
            await context.SaveChangesAsync();

            Assert.That(context.Orders.Count(), Is.EqualTo(2));
        }

        [TestCase(1)]
        public async Task OrderRepository_DeleteByIdAsync_DeleteOrder(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            await orderRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            Assert.That(context.Orders.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task OrderRepository_DeleteAsync_DeleteOrder()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = await orderRepository.GetByIdAsync(1);
            orderRepository.Delete(order);
            await context.SaveChangesAsync();

            Assert.That(context.Orders.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GOrderRepository_Update_UpdateOrder()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var orderRepository = new OrderRepository(context);
            var order = await orderRepository.GetByIdAsync(1);
            order.Comment = "Updated";
            var result = orderRepository.Update(order);
            await context.SaveChangesAsync();

            Assert.That(result, Is.EqualTo(order));
        }
    }
}
