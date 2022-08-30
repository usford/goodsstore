using goodsstore_backend.Models;
using goodsstore_backend.Controllers;
using goodsstore_backend.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace goodsstore_tests.Controllers
{
    [TestClass]
    public class ItemsControllerTest
    {
        [TestClass]
        public class Get
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static ItemsController _itemsController = new ItemsController(_itemsRepository);

            [TestMethod]
            public async Task TestGetAllItems()
            {
                ActionResult<IEnumerable<Item>> actionResult = await _itemsController.Get();

                var objectResult = actionResult.Result as OkObjectResult;
                var items = objectResult?.Value as IEnumerable<Item>;
                
                Assert.AreEqual(_goodsStoreDbContext.Items.Count(), items?.Count());
            }

            [TestMethod]
            [DataRow("10d11558-9161-435b-9187-8c5ff23eec72")]
            public async Task TestGetItemWithId(string id)
            {
                var guidId = new Guid(id);

                ActionResult<Item> actionResult = await _itemsController.Get(guidId);

                Assert.IsNotNull(actionResult.Result as OkObjectResult);
            }
        }

        [TestClass]
        public class Post
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static ItemsController _itemsController = new ItemsController(_itemsRepository);

            [TestMethod]
            [DataRow("подушка-пердушка", 100d, "Голые и смешные")]
            public async Task TestPostItem(string name, double priceDouble, string category)
            {
                var price = Convert.ToDecimal(priceDouble);
                ActionResult<Item> actionResult = await _itemsController.Post(name, price, category);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var item = okObjectResult?.Value as Item;

                Assert.IsNotNull(item);
                Assert.IsNotNull(item.Id);
                Assert.AreEqual(name, item.Name);
                Assert.AreEqual(price, item.Price);
                Assert.AreEqual(category, item.Category);
            }
        }

        [TestClass]
        public class Put
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static ItemsController _itemsController = new ItemsController(_itemsRepository);

            [TestMethod]
            [DataRow("10d11558-9161-435b-9187-8c5ff23eec72")]
            public async Task TestPutItem(string id)
            {
                var item = _goodsStoreDbContext.Items.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(item);

                item.Name = "подушка-хлопушка";

                ActionResult<Item> actionResult = await _itemsController.Put(item);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var newItem = okObjectResult?.Value as Item;

                Assert.IsNotNull(newItem);
                Assert.AreEqual(item.Id, newItem.Id);
                Assert.AreEqual(item.Code, newItem.Code);
                Assert.AreEqual(item.Name, newItem.Name);
                Assert.AreEqual(item.Price, newItem.Price);
                Assert.AreEqual(item.Category, newItem.Category);
            }
        }

        [TestClass]
        public class Delete
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static ItemsRepository _itemsRepository = new ItemsRepository(_goodsStoreDbContext);
            private static ItemsController _itemsController = new ItemsController(_itemsRepository);

            [TestMethod]
            [DataRow("10d11558-9161-435b-9187-8c5ff23eec72")]
            public async Task TestDeleteItem(string id)
            {
                var item = _goodsStoreDbContext.Items.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(item);

                ActionResult<Item> actionResult = await _itemsController.Delete(item.Id);
                var okObjectResult = actionResult.Result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var deleteItem = _goodsStoreDbContext.Items.SingleOrDefault(x => x.Id == item.Id);
                Assert.IsNull(deleteItem);
            }
        }       
    }
}
