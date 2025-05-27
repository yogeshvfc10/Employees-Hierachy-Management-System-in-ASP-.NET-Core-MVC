using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; // For Excel


namespace EmployeeHierachy12345.Controllers
{
    public class CalenderController : Controller
    {
        private readonly ILogger<CalenderController> _logger;
        private readonly EmployeeDbContext _dbcontext;


        public CalenderController(ILogger<CalenderController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _dbcontext.Calender.ToListAsync();
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Calender calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Calender.Add(calendarEvent);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Adjust logging as needed
            }

            // Return to the view with the model
            var events = await _dbcontext.Calender.ToListAsync();
            return View("Index", events);
        }



        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            var calendarEvent = await _dbcontext.Calender.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound(); // Return 404 if the event is not found
            }

            return View(calendarEvent); // Return the event for editing
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Calender calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Calender.Update(calendarEvent); // Update the event
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(calendarEvent); // Return to the view with validation errors
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarEvent = await _dbcontext.Calender.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound(); // Return 404 if the event is not found
            }

            _dbcontext.Calender.Remove(calendarEvent); // Remove the event
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportPeopleInExcel()
        {
            var events = await _dbcontext.Calender.ToListAsync(); // Get calendar events

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Calendar Events");
                worksheet.Cells[1, 1].Value = "Title";
                worksheet.Cells[1, 2].Value = "Start Date";
                worksheet.Cells[1, 3].Value = "End Date";

                int row = 2;
                foreach (var calendarEvent in events)
                {
                    worksheet.Cells[row, 1].Value = calendarEvent.Title;
                    worksheet.Cells[row, 2].Value = calendarEvent.Start;
                    worksheet.Cells[row, 3].Value = calendarEvent.End;
                    row++;
                }

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                var fileName = "CalendarEvents.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
