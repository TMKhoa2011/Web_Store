using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Store.AppData;
using Web_Store.Models;

namespace Web_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Appdbcontext _context;
        public HomeController(ILogger<HomeController> logger, Appdbcontext context)
        {
            _logger = logger;
            _context = context;
        }
         public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();


            return View(products);
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
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel
            {
                TotalAmount = 140
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutViewModel model, string paymentMethod)
        {
            if (!model.AcceptTerms)
            {
                ModelState.AddModelError(nameof(model.AcceptTerms), "Bạn cần đồng ý với Terms of Use trước khi thanh toán.");
            }

            if (ModelState.IsValid)
            {
                TempData["CheckoutSuccess"] = $"Đặt hàng thành công qua {paymentMethod ?? "Checkout"} cho {model.FullName}!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
