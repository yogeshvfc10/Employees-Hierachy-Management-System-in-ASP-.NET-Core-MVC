using DocumentFormat.OpenXml.InkML;
using EmployeeHierachy12345.Models;
using EmployeeHierachy12345.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeHierachy12345.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _service;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly EmployeeDbContext _context;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService service, IHttpContextAccessor httpContextAccessor, EmployeeDbContext context)
        {
            _logger = logger;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public IActionResult Index(Order Order)
        {

            return View();
        }

        public IActionResult Order()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ProcessRequestOrder(PaymentRequest _paymentRequest)
        {
            MerchantOrder _marchantOrder = await _service.ProcessMerchantOrder(_paymentRequest);
            return View("Payment", _marchantOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrderProcess()
        {
            string PaymentMessage = await _service.CompleteOrderProcess(_httpContextAccessor);
            if (PaymentMessage == "captured")
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Failed()
        {
            return View();
        }
    }
}
