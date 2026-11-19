using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
            decimal total = 140m;
            var json = HttpContext.Session.GetString("ShoppingCart");
            if (!string.IsNullOrEmpty(json))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(json);
                if (cart != null && cart.Count > 0)
                {
                    total = cart.Sum(x => x.Total);
                }
            }

            var model = new CheckoutViewModel
            {
                TotalAmount = total
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
                HttpContext.Session.Remove("ShoppingCart");
                TempData["CheckoutSuccess"] = $"Đặt hàng thành công qua {paymentMethod ?? "Checkout"} cho {model.FullName}!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}