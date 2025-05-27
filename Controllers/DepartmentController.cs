using ClosedXML.Excel;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeHierachy12345.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly EmployeeDbContext _dbcontext;


        public DepartmentController(ILogger<DepartmentController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }
        public IActionResult Index()                                       
        {                                                                                                                                           
            var viewModel = _dbcontext.Department.ToList();                                                                                                 
            var employeegrid = (from e in _dbcontext.Department
                                where e.IsDelete == false
                                select new Department
                                {
                                    Id = e.Id,
                                    DepartmentName = e.DepartmentName,
                                    DepartmentID = e.DepartmentID,
                                    Description = e.Description,
                                    DepartmentCode = e.DepartmentCode,
                                    Status = e.Status,
                                }).ToList();
            var d = _dbcontext.Department.ToList();
            var a = new Filter();
            a.Departments = employeegrid;
            //a.Departments = d;
            
            return View(a);
        }
        public IActionResult Create(Filter model)
        {
            var a = new Department
            {
                Id = model.Id,
                DepartmentID = model.department.DepartmentID,
                DepartmentName = model.department.DepartmentName,
                DepartmentCode = model.department.DepartmentCode,
                Description = model.department.Description,
                Status = model.department.Status
            };
            _dbcontext.Department.Add(a);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _dbcontext.Department.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Department.Update(department);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]                                                                                              
        public IActionResult Delete(int Id)                                                                     
        {                                                                                                       
            var a = _dbcontext.Department.FirstOrDefault(e => e.Id == Id);
            if (a != null)
            {
                a.IsDelete = true;
            }
            //_dbcontext.Department.Remove(a);

            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }       

        [HttpGet]
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var employeeRegisters = await _dbcontext.Department.Where(e => e.IsDelete == false).ToListAsync();
            var fileName = "people.xlsx";
            return GenerateExcel(fileName, employeeRegisters);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Department> employeeRegisters)
        {
            DataTable dataTable = new DataTable("People");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("DepartmentID"),
                new DataColumn("DepartmentName"),
                new DataColumn("DepartmentCode"),
                new DataColumn("Description"),
                new DataColumn("Status"),
            });

            foreach (var department in employeeRegisters)
            {
                dataTable.Rows.Add(department.DepartmentID, department.DepartmentName, department.DepartmentCode, department.Description, department.Status);
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
            var a = model.EmployeeRegister.Department;
            var employeegrid = (from e in _dbcontext.EmployeeRegister
                                join c in _dbcontext.Department on e.Department equals c.Id
                                where e.Department == a 
                                select new GeneralModel
                                {
                                    Department = c.DepartmentName,
                                }).ToList();

            var d = _dbcontext.Department.ToList();
            var Filter = new Filter();
            Filter.GeneralModel = employeegrid;
            Filter.Departments = d;
            
            return View("Index", Filter);
        }
    }
}
