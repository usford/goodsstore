using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    [ApiController]
    [Route("customers")]
    [Produces("application/json")]
    public class CustomersContoller : ControllerBase
    {
        public CustomersContoller(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        private readonly ICustomersRepository _customersRepository;
               
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return Ok(await _customersRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            Customer? customer = await _customersRepository.Get(id);

            if (customer is null) return NotFound("Заказчик с таким ID не найден");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(string name, string? address = null, byte discount = 0)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Отсутствует имя у заказчика");

            //Формат XXXX-ГГГГ, где X - число, а ГГГГ - год
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
            if (customer is null) return BadRequest("Заказчик пустой");

            Customer? newCustomer = await _customersRepository.Update(customer);

            if (newCustomer is null) return NotFound("Заказчик с таким ID не найден");

            var regex = new Regex(@"^\d{4}-\d{4}$");

            if (!regex.IsMatch(newCustomer.Code)) return BadRequest("Неверный формат кода");

            await _customersRepository.SaveChangesAsync();

            return Ok(newCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(Guid customerId)
        {
            Customer? customer = await _customersRepository.Remove(customerId);

            if (customer is null) return NotFound("Заказчик с таким ID не найден");

            await _customersRepository.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
