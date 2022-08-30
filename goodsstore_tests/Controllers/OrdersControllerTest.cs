using goodsstore_backend.Models;
using goodsstore_backend.Controllers;
using goodsstore_backend.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace goodsstore_tests.Controllers
{
    [TestClass]
    public class OrdersControllerTest
    {
        [TestClass]
        public class Get
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static OrdersContoller _ordersController = new OrdersContoller(_ordersRepository, _customersRepository);

            [TestMethod]
            public async Task TestGetAllOrders()
            {
                ActionResult<IEnumerable<Order>> actionResult = await _ordersController.Get();

                var objectResult = actionResult.Result as OkObjectResult;
                var orders = objectResult?.Value as IEnumerable<Order>;
                
                Assert.AreEqual(_goodsStoreDbContext.Orders.Count(), orders?.Count());

                foreach (Order order in orders!)
                {
                    if (order.Customer is null)
                    {
                        Assert.IsFalse(true);
                        break;
                    }
                }
            }

            [TestMethod]
            [DataRow("7223681c-b1a1-4187-a3f5-b24211e2634e")]
            public async Task TestGetOrderWithId(string id)
            {
                var guidId = new Guid(id);

                ActionResult<Order> actionResult = await _ordersController.Get(guidId);

                Assert.IsNotNull(actionResult.Result as OkObjectResult);
            }
        }

        [TestClass]
        public class Post
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static OrdersContoller _ordersController = new OrdersContoller(_ordersRepository, _customersRepository);

            [TestMethod]
            [DataRow("ab87cfa0-abaf-4420-acfd-ecc9fb058427")]
            public async Task TestPostOrder(string customerId, DateTime? shipmentDate = null, int orderNumber = 1, string status = "Новый")
            {
                ActionResult<Order> actionResult = await _ordersController.Post(new Guid(customerId), shipmentDate, orderNumber, status);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var order = okObjectResult?.Value as Order;

                Assert.IsNotNull(order);
                Assert.IsNotNull(order.Id);
                Assert.AreEqual(new Guid(customerId), order.CustomerId);
                Assert.IsNotNull(order.Customer);
                Assert.AreEqual(DateTime.Now.Date, order.OrderDate.Date);
                Assert.AreEqual(shipmentDate, order.ShipmentDate);
                Assert.AreEqual(orderNumber, order.OrderNumber);
                Assert.AreEqual(status, order.Status);
            }
        }

        [TestClass]
        public class Put
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static OrdersContoller _ordersController = new OrdersContoller(_ordersRepository, _customersRepository);

            [TestMethod]
            [DataRow("7223681c-b1a1-4187-a3f5-b24211e2634e")]
            public async Task TestPutOrder(string id)
            {
                var order = _goodsStoreDbContext.Orders.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(order);

                order.Status = "Выполнен";

                ActionResult<Order> actionResult = await _ordersController.Put(order);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var newOrder = okObjectResult?.Value as Order;

                Assert.IsNotNull(newOrder);
                Assert.AreEqual(order.Id, newOrder.Id);
                Assert.AreEqual(order.CustomerId, newOrder.CustomerId);
                Assert.AreEqual(order.OrderDate, newOrder.OrderDate);
                Assert.AreEqual(order.ShipmentDate, newOrder.ShipmentDate);
                Assert.AreEqual(order.Status, newOrder.Status);
            }
        }

        [TestClass]
        public class Delete
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static OrdersContoller _ordersController = new OrdersContoller(_ordersRepository, _customersRepository);

            [TestMethod]
            [DataRow("7223681c-b1a1-4187-a3f5-b24211e2634e")]
            public async Task TestDeleteOrder(string id)
            {
                var order = _goodsStoreDbContext.Orders.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(order);

                ActionResult<Order> actionResult = await _ordersController.Delete(order.Id);
                var okObjectResult = actionResult.Result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var deleteOrder = _goodsStoreDbContext.Orders.SingleOrDefault(x => x.Id == order.Id);
                Assert.IsNull(deleteOrder);
            }
        }       
    }
}
