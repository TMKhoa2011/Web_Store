using Microsoft.AspNetCore.Mvc;

namespace Web_Store.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
