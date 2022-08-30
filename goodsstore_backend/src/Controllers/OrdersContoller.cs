﻿using Microsoft.AspNetCore.Mvc;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    [ApiController]
    [Route("orders")]
    [Produces("application/json")]
    public class OrdersContoller : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;
        public OrdersContoller(IOrdersRepository ordersRepository, ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return Ok(await _ordersRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            Order? order = await _ordersRepository.Get(id);

            if (order is null)
            {
                return NotFound("Заказ с таким ID не найден");
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Guid customerId, DateTime? shipmentDate = null, int orderNumber = 1, string status = "Новый")
        {
            Customer? customer = await _customersRepository.Get(customerId);

            if (customer is null)
            {
                return NotFound("Заказчик с таким ID не найден");
            }

            var order = new Order(customerId, DateTime.Now) { 
                ShipmentDate = shipmentDate, 
                OrderNumber = orderNumber, 
                Status = status
            };
            _ordersRepository.Add(order);

            await _ordersRepository.SaveChangesAsync();

            return Ok(order);
        }

        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            if (order is null)
            {
                return BadRequest("Пустой заказчик");
            };

            Order? newOrder = await _ordersRepository.Update(order);

            if (newOrder is null)
            {
                return NotFound("Заказ с таким ID не найден");
            }

            await _ordersRepository.SaveChangesAsync();

            return Ok(newOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(Guid id)
        {
            Order? order = await _ordersRepository.Remove(id);

            if (order is null)
            {
                return NotFound("Заказ с таким ID не найден");
            }

            await _ordersRepository.SaveChangesAsync();

            return Ok(order);
        }
    }
}
