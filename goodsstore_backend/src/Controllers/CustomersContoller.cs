using Microsoft.AspNetCore.Mvc;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
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
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return new ObjectResult(await _customersRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            Customer? customer = await _customersRepository.Get(id);

            if (customer is null)
            {
                return NotFound();
            }

            return new ObjectResult(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(string name, string? address = null, byte discount = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            string leftSideCode = new Random().Next(0, 10000).ToString("0000");
            string rightSideCode = DateTime.Now.Year.ToString();
            var code = $"{leftSideCode}-{rightSideCode}";

            var customer = new Customer(name, code) { Address = address, Discount = discount };
            _customersRepository.Add(customer);

            await _customersRepository.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> Put(Customer customer)
        {
            if (customer is null)
            {
                return BadRequest();
            };

            Customer? newCustomer = await _customersRepository.Update(customer);

            if (newCustomer is null)
            {
                return NotFound();
            }

            await _customersRepository.SaveChangesAsync();

            return Ok(newCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(Guid id)
        {
            Customer? customer = await _customersRepository.Remove(id);

            if (customer is null)
            {
                return NotFound();
            }

            await _customersRepository.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
