using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace EmployeeHierachy12345.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeDbContext _dbcontext;                                                              
        public EmployeeController(ILogger<EmployeeController> logger, EmployeeDbContext dbContext)                  
        {                                                                                                           
            _logger = logger;                                                                                             
            _dbcontext = dbContext;                                                                                 
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new ViewModel();
            var Department = _dbcontext.Department.ToList();
            viewmodel.Department = Department;
            var Region = _dbcontext.Region.ToList();
            viewmodel.Region = Region;
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeRegister employeeRegister)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.EmployeeRegister.Add(employeeRegister);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the Index action                                                  
            }
            var viewmodel = new ViewModel();
            var Department = _dbcontext.Department.ToList();
            viewmodel.Department = Department;
            var Region = _dbcontext.Region.ToList();
            viewmodel.Region = Region;
            return View("Create", viewmodel); // Return the Create view with the view model                                     
        }


        [HttpGet]
        public IActionResult Index()                                                                               
        {                                                                                                               
            var viewModel = _dbcontext.EmployeeRegister.ToList();                                                           
            var employeegrid = (from e in _dbcontext.EmployeeRegister                                               
                                join c in _dbcontext.Department on e.Department equals c.Id
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.IsDelete == false
                                select new GeneralModel
                                {
                                    Id = e.Id,
                                    Name = e.Name,
                                    Email = e.Email,
                                    Gender = e.Gender,
                                    Hiredate = e.Hiredate,
                                    PhoneNo = e.PhoneNo,
                                    State = e.State,
                                    Department = c.DepartmentName,
                                    Region = s.RegionName,
                                    Address = e.Address,
                                    Password = e.Password,
                                    City = e.City,
                                    Salary = e.Salary,
                                    ShiftStartTime = e.ShiftStartTime,
                                    ShiftEndTime = e.ShiftEndTime,
                                    IsActive = e.IsActive,
                                    Comments = e.Comments,
                                    Timespan = DateTime.Now,
                                }).ToList();
            var d = _dbcontext.Department.ToList();
            var r = _dbcontext.Region.ToList();
            var a = new Filter();
            
            a.GeneralModel = employeegrid;
            a.Departments = d;
            a.Regions = r;

            return View(a);
        }

        //Filter
        public IActionResult Filter(Filter model)
        {
            var a = model.EmployeeRegister.Department;
            var b = model.EmployeeRegister.Region;
            var f = model.EmployeeRegister.Name;
            var g = model.EmployeeRegister.Email;

            var employeegrid = (from e in _dbcontext.EmployeeRegister                                               
                                    join c in _dbcontext.Department on e.Department equals c.Id
                                    join s in _dbcontext.Region on e.Region equals s.Id
                                    where e.Department == a || e.Region == b || e.Name == f
                                    where e.IsDelete == false
                                    select new GeneralModel
                                    {
                                        Name = e.Name,
                                        Email = e.Email,
                                        PhoneNo = e.PhoneNo,
                                        Address = e.Address,
                                        City = e.City,
                                        State = e.State,
                                        Gender = e.Gender,
                                        ShiftEndTime = e.ShiftEndTime,
                                        ShiftStartTime = e.ShiftStartTime,
                                        Salary = e.Salary,
                                        Comments = e.Comments,
                                        Department = c.DepartmentName,
                                        Region = s.RegionName,
                                    }).ToList();

            var d = _dbcontext.Department.ToList();
            var r = _dbcontext.Region.ToList();
            
            var Filter = new Filter();
            Filter.GeneralModel = employeegrid;
            Filter.Departments = d;
            Filter.Regions = r;
              
            return View("Index", Filter);
        }

        //clear
        public IActionResult ClearFilter()
        {
            var employeegrid = (from e in _dbcontext.EmployeeRegister                                               
                                join c in _dbcontext.Department on e.Department equals c.Id
                                join s in _dbcontext.Region on e.Region equals s.Id
                                where e.IsDelete == false
                                select new GeneralModel
                                {
                                    Id = e.Id,
                                    Name = e.Name,
                                    Email = e.Email,
                                    Gender = e.Gender,
                                    Hiredate = e.Hiredate,
                                    PhoneNo = e.PhoneNo,
                                    State = e.State,
                                    Department = c.DepartmentName,
                                    Region = s.RegionName,
                                    Address = e.Address,
                                    Password = e.Password,
                                    City = e.City,
                                    Salary = e.Salary,
                                    ShiftStartTime = e.ShiftStartTime,
                                    ShiftEndTime = e.ShiftEndTime,
                                    IsActive = e.IsActive,
                                    Comments = e.Comments,
                                }).ToList();
            var d = _dbcontext.Department.ToList();
            var r = _dbcontext.Region.ToList();
            var a = new Filter();
            a.GeneralModel = employeegrid;

            a.Departments = d;
            a.Regions = r;
            return View("Index",a);
        }
            


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var viewmodel = new ViewModel();
            var Department = _dbcontext.Department.ToList();
            viewmodel.Department = Department;
            var Region = _dbcontext.Region.ToList();
            viewmodel.Region = Region;
            var employee = (from e in _dbcontext.EmployeeRegister
                            join c in _dbcontext.Department on e.Department equals c.Id
                            join s in _dbcontext.Region on e.Region equals s.Id
                            where e.Id == Id
                            select new EmployeeRegister
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Email = e.Email,
                                Gender = e.Gender,
                                Hiredate = e.Hiredate,
                                PhoneNo = e.PhoneNo,
                                State = e.State,
                                Department = e.Department,
                                Region = e.Region,
                                Address = e.Address,
                                Password = e.Password,
                                City = e.City,
                                Salary = e.Salary,
                                ShiftStartTime = e.ShiftStartTime,
                                ShiftEndTime = e.ShiftEndTime,
                                IsActive = e.IsActive,
                                Comments = e.Comments,

                            }).FirstOrDefault();
            viewmodel.EmployeeRegister = employee;
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, EmployeeRegister EmployeeRegister)
        {
            if (Id != EmployeeRegister.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbcontext.EmployeeRegister.Update(EmployeeRegister);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewmodel = new ViewModel();
            var employeeRegisters = _dbcontext.Department.ToList();
            viewmodel.Department = employeeRegisters;
            var sales = _dbcontext.Region.ToList();
            viewmodel.Region = sales;
            return View("Edit", viewmodel);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var employee = (from e in _dbcontext.Analysis
                            join c in _dbcontext.EmployeeRegister on e.Name equals c.Id
                            join s in _dbcontext.Sale on e.Project equals s.Id
                            where e.Id == Id
                            select new AnalysisGeneralModel
                            {
                                Id = e.Id,
                                Name = c.Name,
                                Project = s.Project,
                                Task = e.Task,
                                Date = e.Date,
                                Priority = e.Priority,
                                Status = e.Status,
                                Feedback = e.Feedback,
                                ClientApproval = e.ClientApproval,
                                Module = e.Module,
                                Update = e.Update,
                                AuditLogs = e.AuditLogs,
                                WorkingHrs = e.WorkingHrs,
                                IsDelete = true,
                            }).FirstOrDefault();

            var employee1 = _dbcontext.Analysis.FirstOrDefault(x => x.Id == Id);
            if (employee1 != null)
            {
                employee1.IsDelete = true;
            }
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]                                                                                                   
        public IActionResult Details(int Id)                                                                        
        {                                                                                                           
            var employee = (from e in _dbcontext.EmployeeRegister                                                           
                            join c in _dbcontext.Department on e.Department equals c.Id                                    
                            join s in _dbcontext.Region on e.Region equals s.Id                                   
                            where e.Id == Id                                                                                
                            select new GeneralModel                                                                 
                            {                                                                                              
                                Id = e.Id,                                                                          
                                Name = e.Name,
                                Email = e.Email,
                                Gender = e.Gender,
                                Hiredate = e.Hiredate,
                                PhoneNo = e.PhoneNo,
                                State = e.State,
                                Department = c.DepartmentName,
                                Region = s.RegionName,
                                Address = e.Address,
                                Password = e.Password,
                                City = e.City,
                                Salary = e.Salary,
                                ShiftStartTime = e.ShiftStartTime,
                                ShiftEndTime = e.ShiftEndTime,
                                IsActive = e.IsActive,
                                Comments = e.Comments,
                            }).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var employeeRegisters = await _dbcontext.EmployeeRegister.Where(e=>e.IsDelete==false).ToListAsync();
            var fileName = "people.xlsx";
            return GenerateExcel(fileName, employeeRegisters);
        }
        private FileResult GenerateExcel(string fileName, IEnumerable<EmployeeRegister> employeeRegisters)
        {
            DataTable dataTable = new DataTable("People");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Email"),
                new DataColumn("PhoneNo"),
                new DataColumn("Address"),
                new DataColumn("City"),
                new DataColumn("State"),
                new DataColumn("Hiredate"),
                new DataColumn("Gender"),
                new DataColumn("Region"),
                new DataColumn("Department"),
                new DataColumn("ShiftStartTime"),
                new DataColumn("ShiftEndTime"),
                new DataColumn("Salary"),
                new DataColumn("Comments")
            });

            foreach (var employeeRegister in employeeRegisters)
            {
                dataTable.Rows.Add(employeeRegister.Id, employeeRegister.Name, employeeRegister.Email, employeeRegister.PhoneNo, employeeRegister.Address, employeeRegister.City, employeeRegister.State, employeeRegister.Hiredate,
                    employeeRegister.Gender, employeeRegister.Region, employeeRegister.Department, employeeRegister.ShiftStartTime, employeeRegister.ShiftEndTime, employeeRegister.Salary, employeeRegister.Comments);
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

        private Dictionary<int, GeneralModel> CalculateTimespans(List<GeneralModel> records)
        {
            var timespans = new Dictionary<int, GeneralModel>();
            for (int i = 0; i < records.Count - 1; i++)
            {
                var currentRecord = records[i];
                var nextRecord = records[i + 1];
                var timespan = nextRecord.Timespan - currentRecord.Timespan;
                timespans.Add(currentRecord.Id, new GeneralModel
                {
                    Id = currentRecord.Id,
                    Name = currentRecord.Name,
                    Email = currentRecord.Email,
                    Gender = currentRecord.Gender,
                    Hiredate = currentRecord.Hiredate,
                    PhoneNo = currentRecord.PhoneNo,
                    State = currentRecord.State,
                    Department = currentRecord.Department,
                    Region = currentRecord.Region,
                    Address = currentRecord.Address,
                    Password = currentRecord.Password,
                    City = currentRecord.City,
                    Salary = currentRecord.Salary,
                    ShiftStartTime = currentRecord.ShiftStartTime,
                    ShiftEndTime = currentRecord.ShiftEndTime,
                    IsActive = currentRecord.IsActive,
                    Comments = currentRecord.Comments,
                    Timespan = DateTime.Now,    // Assign the next record's timestamp

                });
            }
            return timespans;
        }
    }
}
