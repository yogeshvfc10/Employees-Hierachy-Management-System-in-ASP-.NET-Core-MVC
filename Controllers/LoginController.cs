using DocumentFormat.OpenXml.InkML;
using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHierachy12345.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly EmployeeDbContext _dbcontext;                                                              
        public LoginController(ILogger<LoginController> logger,  EmployeeDbContext dbContext)                  
        {                                                                                                           
            _logger = logger;
            _dbcontext = dbContext;                                                                                 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminPage model)
        {
            if (ModelState.IsValid)
            {
                var user = await _dbcontext.LoginPage.FirstOrDefaultAsync(u => u.LoginID == model.LoginID && u.Password == model.Password);
                if (user != null)
                {
                    if (user.Password == model.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or password");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or password");
                }
            }
            return View(model);
        }

    }
}
