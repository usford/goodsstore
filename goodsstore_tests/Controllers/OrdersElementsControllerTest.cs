using goodsstore_backend.Models;
using goodsstore_backend.Controllers;
using goodsstore_backend.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace goodsstore_tests.Controllers
{
    [TestClass]
    public class OrdersElementsControllerTest
    {
        [TestClass]
        public class Get
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersElementsRepository _ordersElementsRepository = new OrdersElementsRepository(_goodsStoreDbContext);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static OrdersElementsController _ordersElementsController = new OrdersElementsController(_ordersElementsRepository, _ordersRepository, _itemsRepository);

            [TestMethod]
            public async Task TestGetAllOrdersElements()
            {
                ActionResult<IEnumerable<OrderElement>> actionResult = await _ordersElementsController.Get();

                var objectResult = actionResult.Result as OkObjectResult;
                var ordersElements = objectResult?.Value as IEnumerable<OrderElement>;
                
                Assert.AreEqual(_goodsStoreDbContext.OrdersElements.Count(), ordersElements?.Count());

                foreach (OrderElement orderElement in ordersElements!)
                {
                    if (orderElement.Item is null || orderElement.Order is null)
                    {
                        Assert.IsFalse(true);
                        break;
                    }

                }
            }

            [TestMethod]
            [DataRow("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
            public async Task TestGetOrderElementWithId(string id)
            {
                var guidId = new Guid(id);

                ActionResult<OrderElement> actionResult = await _ordersElementsController.Get(guidId);

                Assert.IsNotNull(actionResult.Result as OkObjectResult);
            }
        }

        [TestClass]
        public class Post
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersElementsRepository _ordersElementsRepository = new OrdersElementsRepository(_goodsStoreDbContext);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static OrdersElementsController _ordersElementsController = new OrdersElementsController(_ordersElementsRepository, _ordersRepository, _itemsRepository);

            [TestMethod]
            [DataRow("7223681c-b1a1-4187-a3f5-b24211e2634e", "10d11558-9161-435b-9187-8c5ff23eec72", 5)]
            public async Task TestPostOrderElement(string orderId, string itemId, int itemsCount)
            {
                ActionResult<OrderElement> actionResult = await _ordersElementsController.Post(new Guid(orderId), new Guid(itemId), itemsCount);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var orderElement = okObjectResult?.Value as OrderElement;

                Assert.IsNotNull(orderElement);
                Assert.IsNotNull(orderElement.Id);
                Assert.AreEqual(new Guid(orderId), orderElement.OrderId);
                Assert.IsNotNull(orderElement.Order);
                Assert.AreEqual(new Guid(itemId), orderElement.ItemId);
                Assert.IsNotNull(orderElement.Item);
                Assert.AreEqual(itemsCount, orderElement.ItemsCount);
            }
        }

        [TestClass]
        public class Put
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersElementsRepository _ordersElementsRepository = new OrdersElementsRepository(_goodsStoreDbContext);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static OrdersElementsController _ordersElementsController = new OrdersElementsController(_ordersElementsRepository, _ordersRepository, _itemsRepository);

            [TestMethod]
            [DataRow("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
            public async Task TestPutOrderElement(string id)
            {
                var orderElement = _goodsStoreDbContext.OrdersElements.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(orderElement);

                orderElement.ItemsCount = 10;

                ActionResult<OrderElement> actionResult = await _ordersElementsController.Put(orderElement);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var newOrderElement = okObjectResult?.Value as OrderElement;

                Assert.IsNotNull(newOrderElement);
                Assert.AreEqual(orderElement.Id, newOrderElement.Id);
                Assert.AreEqual(orderElement.OrderId, newOrderElement.OrderId);
                Assert.AreEqual(orderElement.ItemId, newOrderElement.ItemId);
                Assert.AreEqual(orderElement.ItemsCount, newOrderElement.ItemsCount);
                Assert.AreEqual(orderElement.ItemPrice, newOrderElement.ItemPrice);
            }
        }

        [TestClass]
        public class Delete
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static OrdersElementsRepository _ordersElementsRepository = new OrdersElementsRepository(_goodsStoreDbContext);
            private static OrdersRepository _ordersRepository = new OrdersRepository(_goodsStoreDbContext);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static OrdersElementsController _ordersElementsController = new OrdersElementsController(_ordersElementsRepository, _ordersRepository, _itemsRepository);

            [TestMethod]
            [DataRow("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
            public async Task TestDeleteOrderElement(string id)
            {
                var orderElement = _goodsStoreDbContext.OrdersElements.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(orderElement);

                ActionResult<OrderElement> actionResult = await _ordersElementsController.Delete(orderElement.Id);
                var okObjectResult = actionResult.Result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var deleteOrderElement = _goodsStoreDbContext.OrdersElements.SingleOrDefault(x => x.Id == orderElement.Id);
                Assert.IsNull(deleteOrderElement);
            }
        }       
    }
}
