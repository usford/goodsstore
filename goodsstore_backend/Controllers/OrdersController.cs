using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using goodsstore_backend.Models;
using goodsstore_backend.Enums;
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
        public async Task<ActionResult<IEnumerable<Order>>> Index(
            SortState.Orders sortOrder = SortState.Orders.CustomerNameAsc)
        {
            IEnumerable<Order> orders = await SortSelection(sortOrder);
            
            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Customer> customers = await _customersRepository.Get();

            ViewBag.Customers = new SelectList(customers, "Id", "Code");
            ViewBag.CustomersCount = customers.Count();

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

        public async Task<IEnumerable<Order>> SortSelection(SortState.Orders sortOrder)
        {
            IEnumerable<Order> orders = await _ordersRepository.Get();

            ViewData["CustomerNameSort"] = sortOrder == SortState.Orders.CustomerNameAsc
                ? SortState.Orders.CustomerNameDesc
                : SortState.Orders.CustomerNameAsc;

            ViewData["CustomerCodeSort"] = sortOrder == SortState.Orders.CustomerCodeAsc
                ? SortState.Orders.CustomerCodeDesc
                : SortState.Orders.CustomerCodeAsc;

            ViewData["OrderDateSort"] = sortOrder == SortState.Orders.OrderDateAsc
                ? SortState.Orders.OrderDateDesc
                : SortState.Orders.OrderDateAsc;

            ViewData["ShipmentDateSort"] = sortOrder == SortState.Orders.ShipmentDateAsc
                ? SortState.Orders.ShipmentDateDesc
                : SortState.Orders.ShipmentDateAsc;

            ViewData["OrderNumberSort"] = sortOrder == SortState.Orders.OrderNumberAsc
                ? SortState.Orders.OrderNumberDesc
                : SortState.Orders.OrderNumberAsc;

            ViewData["StatusSort"] = sortOrder == SortState.Orders.StatusAsc
                ? SortState.Orders.StatusDesc
                : SortState.Orders.StatusAsc;

            orders = sortOrder switch
            {
                SortState.Orders.CustomerNameDesc => orders.OrderByDescending(o => o.Customer.Name),
                SortState.Orders.CustomerCodeAsc => orders.OrderBy(o => o.Customer.Code),
                SortState.Orders.CustomerCodeDesc => orders.OrderByDescending(o => o.Customer.Code),
                SortState.Orders.OrderDateAsc => orders.OrderBy(o => o.OrderDate),
                SortState.Orders.OrderDateDesc => orders.OrderByDescending(o => o.OrderDate),
                SortState.Orders.ShipmentDateAsc => orders.OrderBy(o => o.ShipmentDate),
                SortState.Orders.ShipmentDateDesc => orders.OrderByDescending(o => o.ShipmentDate),
                SortState.Orders.OrderNumberAsc => orders.OrderBy(o => o.OrderNumber),
                SortState.Orders.OrderNumberDesc => orders.OrderByDescending(o => o.OrderNumber),
                SortState.Orders.StatusAsc => orders.OrderBy(o => o.Status),
                SortState.Orders.StatusDesc => orders.OrderByDescending(o => o.Status),
                _ => orders.OrderBy(o => o.Customer.Name)
            };

            return orders;
        }
    }
}
