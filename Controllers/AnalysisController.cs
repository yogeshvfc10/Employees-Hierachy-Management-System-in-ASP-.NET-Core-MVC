using ClosedXML.Excel;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Azure.Core.HttpHeader;

namespace EmployeeHierachy12345.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly ILogger<AnalysisController> _logger;
        private readonly EmployeeDbContext _dbcontext;
        public AnalysisController(ILogger<AnalysisController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Fetch data for the analysis view model
            var employeeGrid = (from e in _dbcontext.Analysis
                                join c in _dbcontext.EmployeeRegister on e.Name equals c.Id
                                join s in _dbcontext.Sale on e.Project equals s.Id
                                where e.IsDelete == false
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
                                }).ToList();

            // Load names and projects
            var names = _dbcontext.EmployeeRegister.ToList();
            var projects = _dbcontext.Sale.ToList();

            // Create the filter model
            var analysisFilter = new AnalysisFilter();

            analysisFilter.AnalysisGeneralModel = employeeGrid;
            analysisFilter.Employee = names;
            analysisFilter.Sale = projects;
            

            // Return the view with the model
            return View(analysisFilter);
        }


        [HttpGet]                                                                                                      
        public IActionResult Create()                                                                                  
        {                                                                                                              
            var AnalysisViewModel = new AnalysisViewModel();                                                                           
            var Department = _dbcontext.EmployeeRegister.ToList();                                                           
            AnalysisViewModel.Name = Department;
            var Region = _dbcontext.Sale.ToList();
            AnalysisViewModel.Project = Region;
            return View(AnalysisViewModel);
        }

        [HttpPost]                                                                                                     
        [ValidateAntiForgeryToken]                                                                                                                                                 
        public IActionResult Create(Analysis Analysis)                                                
        {                                                                                                              
            if (ModelState.IsValid)                                                                                          
            {                                                                                                               
                _dbcontext.Analysis.Add(Analysis);                                                                 
                _dbcontext.SaveChanges();                                                                                          
                return RedirectToAction("Index"); // Redirect to the Index action                                                 
            }                                                                                                                
            var AnalysisViewModel = new AnalysisViewModel();
            var Department = _dbcontext.EmployeeRegister.ToList();
            AnalysisViewModel.Name = Department;
            var Region = _dbcontext.Sale.ToList();
            AnalysisViewModel.Project = Region;
            return View("Create", AnalysisViewModel); // Return the Create view with the view model                                     
        }


        [HttpGet]                                                                                                       
        public IActionResult Edit(int Id)                                                                               
        {                                                                                                               
            var viewmodel = new AnalysisViewModel();                                                                                    
            var Department = _dbcontext.EmployeeRegister.ToList();                                                                  
            viewmodel.Name = Department;                                                                                 
            var Region = _dbcontext.Sale.ToList();                                                                                   
            viewmodel.Project = Region;                                                                                          
            var employee = (from e in _dbcontext.Analysis
                            join c in _dbcontext.EmployeeRegister on e.Name equals c.Id
                            join s in _dbcontext.Sale on e.Project equals s.Id                                         
                            where e.Id == Id
                            select new Analysis
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Project = e.Project,
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
                            }).FirstOrDefault();
            viewmodel.Analysis = employee;
            return View(viewmodel);
        }

        [HttpPost]                                                                                                 
        [ValidateAntiForgeryToken]                                                                                 
        public IActionResult Edit(int Id, Analysis Analysis)                                       
        {                                                                                                           
            if (Id != Analysis.Id)                                                                                 
            {                                                                                                    
                return NotFound();                                                                                   
            }                                                                                                      

            if (ModelState.IsValid)                                                                                         
            {                                                                                                       
                _dbcontext.Analysis.Update(Analysis);                                                       
                _dbcontext.SaveChanges();                                                                                
                return RedirectToAction("Index");                                                                            
            }                                                                                                      
            var viewmodel = new AnalysisViewModel();                                                                       
            var employeeRegisters = _dbcontext.EmployeeRegister.ToList();
            viewmodel.Name = employeeRegisters;
            var sales = _dbcontext.Sale.ToList();
            viewmodel.Project = sales;
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
        public async Task<FileResult> ExportPeopleInExcel()
        {
            var employeeRegisters = await _dbcontext.Analysis.Where(e => e.IsDelete == false).ToListAsync();
            var fileName = "people.xlsx";
            return GenerateExcel(fileName, employeeRegisters);
        }
        private FileResult GenerateExcel(string fileName, IEnumerable<Analysis> employeeRegisters)
        {
            DataTable dataTable = new DataTable("People");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Project"),
                new DataColumn("Task"),
                new DataColumn("Date"),
                new DataColumn("Priority"),
                new DataColumn("Status"),
                new DataColumn("Feedback"),
                new DataColumn("ClientApproval"),
                new DataColumn("Module"),
                new DataColumn("Update"),
                new DataColumn("AuditLogs"),
                new DataColumn("WorkingHrs"),
              
            });

            foreach (var employeeRegister in employeeRegisters)
            {
                dataTable.Rows.Add(employeeRegister.Id, employeeRegister.Name, employeeRegister.Project, employeeRegister.Task, employeeRegister.Date, employeeRegister.Priority, employeeRegister.Status, employeeRegister.Feedback,
                    employeeRegister.ClientApproval, employeeRegister.Module, employeeRegister.Update, employeeRegister.AuditLogs, employeeRegister.WorkingHrs);
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

        // Filter
        public IActionResult Filter(AnalysisFilter model)
        {
            var a = model.Analysis.Project;
            var b = model.Analysis.Name;
            var f = model.Analysis.Status;

            // Ensure filtering logic is correct
            var analysisGeneralModel = (from e in _dbcontext.Analysis
                                        join c in _dbcontext.EmployeeRegister on e.Name equals c.Id
                                        join s in _dbcontext.Sale on e.Project equals s.Id
                                        where !e.IsDelete && (e.Project == a || e.Name == b || e.Status == f)
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
                                        }).ToList();

            var employees = _dbcontext.EmployeeRegister.ToList();
            var sales = _dbcontext.Sale.ToList();

            var analysisFilter = new AnalysisFilter
            {
                AnalysisGeneralModel = analysisGeneralModel,
                Employee = employees,
                Sale = sales
            };

            return View("Index", analysisFilter);
        }

        // Clear
        public IActionResult ClearFilter()
        {
            // Fetch all analysis records that are not deleted
            var analysisGrid = (from e in _dbcontext.Analysis
                                join c in _dbcontext.EmployeeRegister on e.Name equals c.Id
                                join s in _dbcontext.Sale on e.Project equals s.Id
                                where !e.IsDelete
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
                                    WorkingHrs = e.WorkingHrs
                                }).ToList();

            var departments = _dbcontext.EmployeeRegister.ToList();
            var sales = _dbcontext.Sale.ToList();

            var analysisFilter = new AnalysisFilter
            {
                AnalysisGeneralModel = analysisGrid,
                Employee = departments,
                Sale = sales
            };

            return View("Index", analysisFilter);
        }


    }
}
