using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using goodsstore_backend.Models;
using goodsstore_backend.Enums;
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
        public async Task<ActionResult<IEnumerable<Item>>> Index(
            SortState.Items sortOrder = SortState.Items.CodeAsc
            )
        {
            IEnumerable<Item> items = await SortSelection(sortOrder);

            return View(items);
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

        public async Task<IEnumerable<Item>> SortSelection(SortState.Items sortOrder)
        {
            IEnumerable<Item> items = await _itemsRepository.Get();

            ViewData["CodeSort"] = sortOrder == SortState.Items.CodeAsc
                ? SortState.Items.CodeDesc
                : SortState.Items.CodeAsc;

            ViewData["NameSort"] = sortOrder == SortState.Items.NameAsc
                ? SortState.Items.NameDesc
                : SortState.Items.NameAsc;

            ViewData["PriceSort"] = sortOrder == SortState.Items.PriceAsc
                ? SortState.Items.PriceDesc
                : SortState.Items.PriceAsc;

            ViewData["CategorySort"] = sortOrder == SortState.Items.CategoryAsc
                ? SortState.Items.CategoryDesc
                : SortState.Items.CategoryAsc;

            items = sortOrder switch
            {
                SortState.Items.CodeDesc => items.OrderByDescending(i => i.Code),
                SortState.Items.NameAsc => items.OrderBy(i => i.Name),
                SortState.Items.NameDesc => items.OrderByDescending(i => i.Name),
                SortState.Items.PriceAsc => items.OrderBy(i => i.Price),
                SortState.Items.PriceDesc => items.OrderByDescending(i => i.Price),
                SortState.Items.CategoryAsc => items.OrderBy(i => i.Category),
                SortState.Items.CategoryDesc => items.OrderByDescending(i => i.Category),
                _ => items.OrderBy(i => i.Code)
            };

            return items;
        }
    }
}
