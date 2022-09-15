using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    public class OrdersElementsController : Controller
    {
        public OrdersElementsController(
            IOrdersElementsRepository ordersElementsRepository,
            IOrdersRepository ordersRepository,
            IItemsRepository itemsRepository)
        {
            _ordersElementsRepository = ordersElementsRepository;
            _ordersRepository = ordersRepository;
            _itemsRepository = itemsRepository;
        }

        private readonly IOrdersElementsRepository _ordersElementsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IItemsRepository _itemsRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderElement>>> Index()
        {
            return View(await _ordersElementsRepository.Get());
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Order> orders = await _ordersRepository.Get();
            IEnumerable<Item> items = await _itemsRepository.Get();

            ViewBag.Orders = new SelectList(orders, "Id", "OrderNumber");
            ViewBag.Items = new SelectList(items, "Id", "Code");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]OrderElement orderElement)
        {
            _ordersElementsRepository.Add(orderElement);

            await _ordersElementsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            IEnumerable<Order> orders = await _ordersRepository.Get();
            IEnumerable<Item> items = await _itemsRepository.Get();
            OrderElement? orderElement = await _ordersElementsRepository.Get(id);

            if (orderElement != null)
            {
                ViewBag.Orders = new SelectList(orders, "Id", "OrderNumber");
                ViewBag.Items = new SelectList(items, "Id", "Code");
                return View(orderElement);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderElement orderElement)
        {
            await _ordersElementsRepository.Update(orderElement);

            await _ordersElementsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ordersElementsRepository.Remove(id);

            await _ordersElementsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
