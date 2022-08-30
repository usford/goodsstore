using Microsoft.AspNetCore.Mvc;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    [ApiController]
    [Route("items")]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        private readonly IItemsRepository _itemsRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            return Ok(await _itemsRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(Guid id)
        {
            Item? item = await _itemsRepository.Get(id);

            if (item is null)
            {
                return NotFound("Товар с таким ID не найден");
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Post(string name, decimal price, string category)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Отсутствует наименование товара");
            }

            if (price <= 0)
            {
                return BadRequest("Неверная цена");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Отсутствует категория");
            }

            var randomNumber = () => new Random().Next(1, 10);

            var randomUnicodeChar = () => (char)new Random().Next(65, 90);

            //формат кода XX-XXXX-YYXX, где X - число, а Y - заглавная буква алфавита
            string code = $"{randomNumber()}{randomNumber()}" +
                $"-{randomNumber()}{randomNumber()}{randomNumber()}{randomNumber()}" +
                $"-{randomUnicodeChar()}{randomUnicodeChar()}{randomNumber()}{randomNumber()}";

            var item = new Item(code, name, price, category);
            _itemsRepository.Add(item);

            await _itemsRepository.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Item>> Put(Item item)
        {
            if (item is null)
            {
                return BadRequest("Товар пустой");
            }

            Item? newItem = await _itemsRepository.Update(item);

            if (newItem is null)
            {
                return NotFound("Товар с таким ID не найден");
            }

            await _itemsRepository.SaveChangesAsync();

            return Ok(newItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(Guid id)
        {
            Item? item = await _itemsRepository.Remove(id);

            if (item is null)
            {
                return NotFound("Товар с таким ID не найден");
            }

            await _itemsRepository.SaveChangesAsync();

            return Ok(item);
        }
    }
}
