using DocumentFormat.OpenXml.InkML;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHierachy12345.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly EmployeeDbContext _dbcontext;

        public ReportsController(ILogger<ReportsController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var salesData = await _dbcontext.Sale.ToListAsync();
            var chartData = salesData.GroupBy(s => s.Project)
                                      .Select(g => new
                                      {
                                          Project = g.Key,
                                          TotalSales = g.Sum(s => decimal.Parse(s.Price)) // Assuming Price is stored as string
                                      }).ToList();

            ViewBag.ChartData = chartData;

            var sales = _dbcontext.Sale.ToList();
            return View(sales); // Ensure this view is expecting a List<Sale>
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Add(sale);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }


        // GET: Sale/Edit/5
        public IActionResult Edit(int id)
        {
            var sale = _dbcontext.Sale.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        // POST: Sale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(sale);
                    _dbcontext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbcontext.Sale.Any(e => e.Id == sale.Id))
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
            return View(sale); // Return the same view if the model state is not valid
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sale = _dbcontext.Sale.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            _dbcontext.Sale.Remove(sale);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(Index)); // Redirect to the list or another action after deletion
        }

    }
}
