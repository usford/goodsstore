using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using goodsstore_backend.Models;
using goodsstore_backend.Enums;
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
        public async Task<ActionResult<IEnumerable<Customer>>> Index(
            SortState.Customers sortOrder = SortState.Customers.NameAsc
            )
        {
            IEnumerable<Customer> customers = await SortSelection(sortOrder);

            return View(customers);
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

        public async Task<IEnumerable<Customer>> SortSelection(SortState.Customers sortOrder)
        {
            IEnumerable<Customer> customers = await _customersRepository.Get();

            ViewData["NameSort"] = sortOrder == SortState.Customers.NameAsc
                ? SortState.Customers.NameDesc
                : SortState.Customers.NameAsc;

            ViewData["CodeSort"] = sortOrder == SortState.Customers.CodeAsc
                ? SortState.Customers.CodeDesc
                : SortState.Customers.CodeAsc;

            ViewData["AddressSort"] = sortOrder == SortState.Customers.AddressAsc
                ? SortState.Customers.AddressDesc
                : SortState.Customers.AddressAsc;

            ViewData["DiscoutSort"] = sortOrder == SortState.Customers.DiscountAsc
                ? SortState.Customers.DiscountDesc
                : SortState.Customers.DiscountAsc;

            customers = sortOrder switch
            {
                SortState.Customers.NameDesc => customers.OrderByDescending(c => c.Name),
                SortState.Customers.CodeAsc => customers.OrderBy(c => c.Code),
                SortState.Customers.CodeDesc => customers.OrderByDescending(c => c.Code),
                SortState.Customers.AddressAsc => customers.OrderBy(c => c.Address),
                SortState.Customers.AddressDesc => customers.OrderByDescending(c => c.Address),
                SortState.Customers.DiscountAsc => customers.OrderBy(c => c.Discount),
                SortState.Customers.DiscountDesc => customers.OrderByDescending(c => c.Discount),
                _ => customers.OrderBy(c => c.Name)
            };

            return customers;
        }
    }
}
