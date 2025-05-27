using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeHierachy12345.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }

        public IActionResult Index()
        {
            var EmployeeCount = _dbcontext.EmployeeRegister.ToList();
            var CareerCount = _dbcontext.Career.ToList();
            var OrderCount = _dbcontext.Item.ToList();
            var ReportsCount = _dbcontext.Sale.ToList();
        
            var dashboarddata = new Dashboarddata();
            dashboarddata.EmployeeCount = EmployeeCount.Count();
            dashboarddata.CareerCount = CareerCount.Count();
            dashboarddata.OrderCount = OrderCount.Count();
            dashboarddata.ReportsCount = ReportsCount.Count();
            return View(dashboarddata);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult Employeechart()
        {
            // Use your database context to query employee data
            var activeCount = _dbcontext.EmployeeRegister.Count(x => !x.IsDelete);
            var inactiveCount = _dbcontext.EmployeeRegister.Count(x => x.IsDelete);

            // Return the data in JSON format
            var data = new
            {
                labels = new[] { "Active", "Inactive" }, // Labels for the chart
                data = new[] { activeCount, inactiveCount } // Data for the chart
            };

            return Json(data);
        }

        public JsonResult Departmentchart()
        { 
            var IsTrue = _dbcontext.Department.Count(x => !x.IsDelete);
            var IsFalse = _dbcontext.Department.Count(x => x.IsDelete);

            var data = new
            {
                labels = new[] { "Active", "Inactive" },
                data = new[] { IsTrue, IsFalse }
            };

            return Json(data);
        }


        public JsonResult Regionchart()
        {
            var IsTrue = _dbcontext.Region.Count(x => !x.IsDelete);
            var IsFalse = _dbcontext.Region.Count(x => x.IsDelete);

            var data = new
            {
                labels = new[] { "Active", "Inactive" },
                data = new[] { IsTrue, IsFalse }
            };

            return Json(data);
        }
    }
}
