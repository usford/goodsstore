using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.src.Controllers
{
    [ApiController]
    [Route("customers")]
    [Produces("application/json")]
    public class CustomersContoller : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;
        public CustomersContoller(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
        
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customersRepository.GetAll().ToArray();
        }

        [HttpGet("{id}")]
        public Customer? Get(Guid id)
        {
            Customer? customer = _customersRepository.SingleOrDefault(id);

            return customer;
        }

        [HttpPost]
        public Customer? Post(string name, string? address = null, byte discount = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            string code = CreateCode();

            var customer = new Customer(name, code) { Address = address, Discount = discount };
            _customersRepository.Add(customer);

            return customer;
        }

        [HttpPut]
        public Customer? Put(Customer customer)
        {
            if (customer is null) return null;

            Customer? newCustomer = _customersRepository.Update(customer);

            return newCustomer;
        }

        [HttpDelete("{id}")]
        public Customer? Delete(Guid id)
        {
            Customer? customer = _customersRepository.Remove(id);

            return customer;
        }

        private string CreateCode()
        {
            string leftSideCode = new Random().Next(0, 10000).ToString("0000");
            string rightSideCode = DateTime.Now.Year.ToString();
            var code = $"{leftSideCode}-{rightSideCode}";

            return code;
        }
    }
}
