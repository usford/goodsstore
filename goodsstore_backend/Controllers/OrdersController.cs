using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    public class OrdersController : Controller
    {
        public OrdersController(IOrdersRepository ordersRepository, ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Index()
        {
            return View(await _ordersRepository.Get());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = new SelectList(await _customersRepository.Get(), "Id", "Code");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Order order)
        {
            _ordersRepository.Add(order);

            await _ordersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Order? order = await _ordersRepository.Get(id);

            if (order != null)
            {
                ViewBag.Customers = new SelectList(await _customersRepository.Get(), "Id", "Code", order.CustomerId);
                return View(order);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            await _ordersRepository.Update(order);

            await _ordersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ordersRepository.Remove(id);

            await _ordersRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
