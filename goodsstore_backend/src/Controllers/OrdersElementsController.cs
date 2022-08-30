using Microsoft.AspNetCore.Mvc;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    [ApiController]
    [Route("orderselements")]
    [Produces("application/json")]
    public class OrdersElementsController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<OrderElement>>> Get()
        {
            return Ok(await _ordersElementsRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderElement>> Get(Guid id)
        {
            OrderElement? orderElement = await _ordersElementsRepository.Get(id);

            if (orderElement is null) return NotFound("Элемент заказа с таким ID не найден");

            return Ok(orderElement);
        }

        [HttpPost]
        public async Task<ActionResult<OrderElement>> Post(Guid orderId, Guid itemId, int itemsCount)
        {
            Order? order = await _ordersRepository.Get(orderId);
            if (order is null) return NotFound("Заказ с таким ID не найден");

            Item? item = await _itemsRepository.Get(itemId);
            if (item is null) return NotFound("Товар с таким ID не найден");

            if (itemsCount <= 0) return BadRequest("Неверное количество товара");

            if (item.Price <= 0) return BadRequest("Неверная цена товара");

            var orderElement = new OrderElement(orderId, itemId, itemsCount, item.Price);
            _ordersElementsRepository.Add(orderElement);

            await _ordersElementsRepository.SaveChangesAsync();

            return Ok(orderElement);
        }

        [HttpPut]
        public async Task<ActionResult<OrderElement>> Put(OrderElement orderElement)
        {
            if (orderElement is null) return BadRequest("Пустой элемент заказа");

            Order? order = await _ordersRepository.Get(orderElement.OrderId);
            if (order is null) return NotFound("Заказ с таким ID не найден");

            Item? item = await _itemsRepository.Get(orderElement.ItemId);
            if (item is null) return NotFound("Товар с таким ID не найден");

            if (orderElement.ItemsCount <= 0) return BadRequest("Неверное количество товара");

            if (orderElement.ItemPrice <= 0) return BadRequest("Неверная цена товара");

            OrderElement? newOrderElement = await _ordersElementsRepository.Update(orderElement);

            if (newOrderElement is null) return NotFound("Элемент заказа с таким ID не найден");

            await _ordersElementsRepository.SaveChangesAsync();

            return Ok(newOrderElement);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderElement>> Delete(Guid orderElementId)
        {
            OrderElement? orderElement = await _ordersElementsRepository.Remove(orderElementId);

            if (orderElement is null) return NotFound("Элемент заказа с таким ID не найден");

            await _ordersElementsRepository.SaveChangesAsync();

            return Ok(orderElement);
        }
    }
}
