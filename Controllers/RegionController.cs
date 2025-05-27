using ClosedXML.Excel;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace EmployeeHierachy12345.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILogger<RegionController> _logger;
        private readonly EmployeeDbContext _dbcontext;                                                              
        public RegionController(ILogger<RegionController> logger, EmployeeDbContext dbContext)                  
        {                                                                                                           
            _logger = logger;                                                                                            
            _dbcontext = dbContext;                                                                                 
        }
        public IActionResult Index()                                                                                                           //Public IActionResult Index()
        {                                                                                                                                      //{
            var viewModel = _dbcontext.Region.ToList();                                                                                               //return View(_context.Employees.ToList());
            var employeegrid = (from e in _dbcontext.Region
                                where e.IsDelete == false                                                                                       //}
                                select new Region
                                {
                                    Id = e.Id,
                                    RegionID = e.RegionID,
                                    Busineesline = e.Busineesline,
                                    Timezone = e.Timezone,
                                    pincode = e.pincode,
                                    IsDelete = e.IsDelete,
                                    RegionName = e.RegionName,
                                }).ToList();
            var d = _dbcontext.Region.ToList();
            var a = new Filter();
            a.Regions = employeegrid;
            //a.Departments = d;
            
            return View(a);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var region = _dbcontext.Region.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        [HttpPost]
        public IActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Region.Update(region);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        public IActionResult Save(Filter model)
        {
            var z = new Region
            {
                Id = model.Id,
                RegionID = model.region.RegionID,
                RegionName = model.region.RegionName,
                Timezone = model.region.Timezone,
                pincode = model.region.pincode,
                Busineesline = model.region.Busineesline
            };
            _dbcontext.Region.Add(z);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var a = _dbcontext.Region.FirstOrDefault(e => e.Id == Id);
            if (a != null)
            {
                a.IsDelete = true;
            }
            //_dbcontext.Region.Remove(a);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var Region = await _dbcontext.Region.Where(e => e.IsDelete == false).ToListAsync();
            var fileName = "Region.xlsx";
            return GenerateExcel(fileName, Region);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Region> employeeRegisters)
        {
            DataTable dataTable = new DataTable("Region");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("RegionID"),
                new DataColumn("RegionName"),
                new DataColumn("Busineesline"),
                new DataColumn("Timezone"),
                new DataColumn("pincode")
            });

            foreach (var region in employeeRegisters)
            {
                dataTable.Rows.Add(region.RegionID, region.RegionName, region.Busineesline, region.Timezone, region.pincode);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }

        public IActionResult Dropdown(Filter model)
        {
            var b = model.EmployeeRegister.Region;
            var f = model.EmployeeRegister.Name;
            var employeegrid = (from e in _dbcontext.EmployeeRegister
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.Region == b || e.Name == f
                                select new GeneralModel
                                {
                                    Region = s.RegionName,
                                }).ToList();

            var r = _dbcontext.Region.ToList();

            var Filter = new Filter();
            Filter.GeneralModel = employeegrid;
            Filter.Regions = r;

            return View("Index", Filter);
        }  

    }
}
