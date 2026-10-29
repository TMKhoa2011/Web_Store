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
    }
}
