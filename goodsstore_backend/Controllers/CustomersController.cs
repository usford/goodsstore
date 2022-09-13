using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    public class CustomersController : Controller
    {
        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        private readonly ICustomersRepository _customersRepository;
               
        [HttpGet]      
        public async Task<ActionResult<IEnumerable<Customer>>> Index()
        {
            return View(await _customersRepository.Get());
            //return View("Post");
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name)) return BadRequest("Отсутствует имя у заказчика");

            _customersRepository.Add(customer);

            await _customersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Customer? customer = await _customersRepository.Get(id);

            if (customer != null) return View(customer);

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Edit(Customer customer)
        {
            if (customer is null) return BadRequest("Заказчик пустой");

            Customer? newCustomer = await _customersRepository.Update(customer);

            if (newCustomer is null) return NotFound("Заказчик с таким ID не найден");

            //var regex = new Regex(@"^\d{4}-\d{4}$");

            //if (!regex.IsMatch(newCustomer.Code)) return BadRequest("Неверный формат кода");

            await _customersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            Customer? customer = await _customersRepository.Remove(id);

            if (customer is null) return NotFound("Заказчик с таким ID не найден");

            await _customersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
