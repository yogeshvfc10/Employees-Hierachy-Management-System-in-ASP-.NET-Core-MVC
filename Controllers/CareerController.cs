using ClosedXML.Excel;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeHierachy12345.Controllers
{
    public class CareerController : Controller
    {
        private readonly ILogger<CareerController> _logger;
        private readonly EmployeeDbContext _dbcontext;
        public CareerController(ILogger<CareerController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employeeGrid = (from e in _dbcontext.Career
                                join c in _dbcontext.Department on e.Department equals c.Id
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.IsDelete == false
                                select new CareerGeneralModel
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Qualification = e.Qualification,
                                    Benefits = e.Benefits,
                                    Application = e.Application,
                                    Contacts = e.Contacts,
                                    Policy = e.Policy,
                                    Department = c.DepartmentName,
                                    Region = s.RegionName,
                                    Deadlines = e.Deadlines,
                                    ForwardMail = e.ForwardMail,
                                }).ToList();

            var departments = _dbcontext.Department.ToList();
            var regions = _dbcontext.Region.ToList();

            var careerFilter = new CareerFilter
            {
                CareerGeneralModel = employeeGrid,
                Departments = departments,
                Regions = regions
            };

            return View(careerFilter);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var careerViewModel = new CareerViewModel
            {
                Department = _dbcontext.Department.ToList(),
                Region = _dbcontext.Region.ToList()
            };

            return View(careerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Career career)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Career.Add(career);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the Index action
            }

            // If we reach here, something went wrong; repopulate the view model
            var careerViewModel = new CareerViewModel
            {
                Department = _dbcontext.Department.ToList(),
                Region = _dbcontext.Region.ToList(),
                // Optionally populate the existing career info to keep user input
                Career = career // Assuming you want to keep user input
            };

            return View("Create", careerViewModel); // Return the Create view with the view model
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var viewmodel = new CareerViewModel();
            var Department = _dbcontext.Department.ToList();
            viewmodel.Department = Department;
            var Region = _dbcontext.Region.ToList();
            viewmodel.Region = Region;
            var employee = (from e in _dbcontext.Career
                            join c in _dbcontext.Department on e.Department equals c.Id
                            join s in _dbcontext.Region on e.Region equals s.Id
                            where e.Id == Id
                            select new Career
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Description = e.Description,
                                Qualification = e.Qualification,
                                Benefits = e.Benefits,
                                Application = e.Application,
                                Contacts = e.Contacts,
                                Policy = e.Policy,
                                Department = e.Department,
                                Region = e.Region,
                                Deadlines = e.Deadlines,
                                ForwardMail = e.ForwardMail
                            }).FirstOrDefault();
            viewmodel.Career = employee;
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, Career Career)
        {
            if (Id != Career.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbcontext.Career.Update(Career);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            var CareerViewModel = new CareerViewModel();
            var Department = _dbcontext.Department.ToList();
            CareerViewModel.Department = Department;
            var Region = _dbcontext.Region.ToList();
            CareerViewModel.Region = Region;
            return View("Edit", CareerViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var employee = (from e in _dbcontext.Career
                            join c in _dbcontext.Department on e.Id equals c.Id
                            join s in _dbcontext.Region on e.Region equals s.Id
                            where e.Id == Id
                            select new CareerGeneralModel
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Description = e.Description,
                                Qualification = e.Qualification,
                                Benefits = e.Benefits,
                                Application = e.Application,
                                Contacts = e.Contacts,
                                Policy = e.Policy,
                                Department = c.DepartmentName,
                                Region = s.RegionName,
                                Deadlines = e.Deadlines,
                                ForwardMail = e.ForwardMail,
                                IsDelete = true,
                            }).FirstOrDefault();

            var career = _dbcontext.Career.FirstOrDefault(x => x.Id == Id);
            if (career != null)
            {
                career.IsDelete = true;
            }
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var employeeRegisters = await _dbcontext.Career.Where(e => e.IsDelete == false).ToListAsync();
            var fileName = "people.xlsx";
            return GenerateExcel(fileName, employeeRegisters);
        }
        private FileResult GenerateExcel(string fileName, IEnumerable<Career> employeeRegisters)
        {
            DataTable dataTable = new DataTable("People");
            dataTable.Columns.AddRange(new DataColumn[]
            {
               
                new DataColumn("Title"),
                new DataColumn("Description"),
                new DataColumn("Qualification"),
                new DataColumn("Benefits"),
                new DataColumn("Application"),
                new DataColumn("Contacts"),
                new DataColumn("Policy"),
                new DataColumn("Region"),
                new DataColumn("Department"),
                new DataColumn("Deadlines"),
                new DataColumn("ForwardMail"),
            });

            foreach (var employeeRegister in employeeRegisters)
            {
                dataTable.Rows.Add( employeeRegister.Title, employeeRegister.Description, employeeRegister.Qualification, employeeRegister.Benefits, employeeRegister.Application, employeeRegister.Contacts,
                    employeeRegister.Policy, employeeRegister.Region, employeeRegister.Department, employeeRegister.Deadlines, employeeRegister.ForwardMail);
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

        //Filter
        public IActionResult Filter(CareerFilter model)
        {
            var a = model.Careers.Department;
            var b = model.Careers.Region;
            var f = model.Careers.Title;

            var employeegrid = (from e in _dbcontext.Career
                                join c in _dbcontext.Department on e.Department equals c.Id
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.Department == a || e.Region == b || e.Title == f
                                where e.IsDelete == false
                                select new CareerGeneralModel
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Qualification = e.Qualification,
                                    Benefits = e.Benefits,
                                    Application = e.Application,
                                    Contacts = e.Contacts,
                                    Policy = e.Policy,
                                    Department = c.DepartmentName,
                                    Region = s.RegionName,
                                    Deadlines = e.Deadlines,
                                    ForwardMail = e.ForwardMail,
                                }).ToList();
            var d = _dbcontext.Department.ToList();
            var r = _dbcontext.Region.ToList();

            var Filter = new CareerFilter();
            Filter.CareerGeneralModel = employeegrid;
            Filter.Departments = d;
            Filter.Regions = r;

            return View("Index", Filter);
        }

        //clear
        public IActionResult ClearFilter()
        {
            var employeegrid = (from e in _dbcontext.Career
                                join c in _dbcontext.Department on e.Department equals c.Id
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.IsDelete == false
                                select new CareerGeneralModel
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Qualification = e.Qualification,
                                    Benefits = e.Benefits,
                                    Application = e.Application,
                                    Contacts = e.Contacts,
                                    Policy = e.Policy,
                                    Department = c.DepartmentName,
                                    Region = s.RegionName,
                                    Deadlines = e.Deadlines,
                                    ForwardMail = e.ForwardMail,

                                }).ToList();
            var d = _dbcontext.Department.ToList();
            var r = _dbcontext.Region.ToList();
            
            var a = new CareerFilter();
            a.CareerGeneralModel = employeegrid;
            a.Departments = d;
            a.Regions = r;
            return View("Index", a);
        }

    }
}
