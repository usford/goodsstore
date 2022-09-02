using goodsstore_backend.Models;
using goodsstore_backend.Controllers;
using goodsstore_backend.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace goodsstore_tests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        [TestClass]
        public class Get
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static CustomersController _customersController = new CustomersController(_customersRepository);

            [TestMethod]
            public async Task TestGetAllCustomers()
            {
                ActionResult<IEnumerable<Customer>> actionResult = await _customersController.Get();

                var objectResult = actionResult.Result as OkObjectResult;
                var customers = objectResult?.Value as IEnumerable<Customer>;

                Assert.AreEqual(_goodsStoreDbContext.Customers.Count(), customers?.Count());
            }

            [TestMethod]
            [DataRow("ab87cfa0-abaf-4420-acfd-ecc9fb058427")]
            public async Task TestGetCustomerWithId(string id)
            {
                var guidId = new Guid(id);

                ActionResult<Customer> actionResult = await _customersController.Get(guidId);

                Assert.IsNotNull(actionResult.Result as OkObjectResult);
            }
        }

        [TestClass]
        public class Post
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static CustomersController _customersController = new CustomersController(_customersRepository);

            [TestMethod]
            [DataRow("Max King")]
            [DataRow("Max King", "г. Лобня")]
            [DataRow("Max King", "г. Лобня", (byte)30)]
            public async Task TestPostCustomer(string name, string? address = null, byte discount = 0)
            {
                ActionResult<Customer> actionResult = await _customersController.Post(name, address, discount);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var customer = okObjectResult?.Value as Customer;

                var regex = new Regex(@"^\d{4}-\d{4}$");

                Assert.IsNotNull(customer);
                Assert.IsNotNull(customer.Id);
                Assert.AreEqual(name, customer.Name);
                Assert.IsNotNull(customer.Code);
                Assert.AreEqual(true, regex.IsMatch(customer.Code));
                Assert.AreEqual(address, customer.Address);
                Assert.AreEqual(discount, customer.Discount);
            }
        }

        [TestClass]
        public class Put
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static CustomersController _customersController = new CustomersController(_customersRepository);

            [TestMethod]
            [DataRow("ab87cfa0-abaf-4420-acfd-ecc9fb058427")]
            public async Task TestPutCustomer(string id)
            {
                var customer = _goodsStoreDbContext.Customers.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(customer);

                customer.Name = "Vlad";

                ActionResult<Customer> actionResult = await _customersController.Put(customer);

                var okObjectResult = actionResult.Result as OkObjectResult;
                var newCustomer = okObjectResult?.Value as Customer;

                Assert.IsNotNull(newCustomer);
                Assert.AreEqual(customer.Id, newCustomer.Id);
                Assert.AreEqual(customer.Name, newCustomer.Name);
                Assert.AreEqual(customer.Code, newCustomer.Code);
                Assert.AreEqual(customer.Address, newCustomer.Address);
                Assert.AreEqual(customer.Discount, newCustomer.Discount);
            }
        }

        [TestClass]
        public class Delete
        {
            private static DbContextOptions<GoodsStoreDbContextTest> _options = new DbContextOptions<GoodsStoreDbContextTest>();

            private static GoodsStoreDbContextTest _goodsStoreDbContext = new GoodsStoreDbContextTest(_options);
            private static CustomersRepository _customersRepository = new CustomersRepository(_goodsStoreDbContext);
            private static CustomersController _customersController = new CustomersController(_customersRepository);

            [TestMethod]
            [DataRow("ab87cfa0-abaf-4420-acfd-ecc9fb058427")]
            public async Task TestDeleteCustomer(string id)
            {
                var customer = _goodsStoreDbContext.Customers.SingleOrDefault(x => x.Id == new Guid(id));
                Assert.IsNotNull(customer);

                ActionResult<Customer> actionResult = await _customersController.Delete(customer.Id);
                var okObjectResult = actionResult.Result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var deleteCustomer = _goodsStoreDbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);
                Assert.IsNull(deleteCustomer);
            }
        }       
    }
}
