using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHierachy12345.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly EmployeeDbContext _dbcontext;

        public ItemController(ILogger<ItemController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            var sales = _dbcontext.Item.ToList();
            return View(sales); // Ensure this view is expecting a List<Sale>
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item Item1)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Add(Item1);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Item1);
        }

        // GET: Sale/Edit/5
        public IActionResult Edit(int id)
        {
            var Item1 = _dbcontext.Item.Find(id);
            if (Item1 == null)
            {
                return NotFound();
            }
            return View(Item1);
        }

        // POST: Sale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Item Item1)
        {
            if (id != Item1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(Item1);
                    _dbcontext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbcontext.Item.Any(e => e.Id == Item1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the list or another action after editing
            }
            return View(Item1); // Return the same view if the model state is not valid
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Item1 = _dbcontext.Item.Find(id);
            if (Item1 == null)
            {
                return NotFound();
            }

            _dbcontext.Item.Remove(Item1);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(Index)); // Redirect to the list or another action after deletion
        }
    }
}
