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
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Customer customer)
        {
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
        public async Task<IActionResult> Edit(Customer customer)
        {
            await _customersRepository.Update(customer);

            await _customersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customersRepository.Remove(id);

            await _customersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
