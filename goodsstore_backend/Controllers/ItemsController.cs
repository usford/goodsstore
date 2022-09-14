using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        private readonly IItemsRepository _itemsRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Index()
        {
            return View(await _itemsRepository.Get());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Item item)
        {
            _itemsRepository.Add(item);

            await _itemsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Item? item = await _itemsRepository.Get(id);

            if (item != null) return View(item);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            await _itemsRepository.Update(item);

            await _itemsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itemsRepository.Remove(id);

            await _itemsRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
