using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Store.AppData;
using Web_Store.Data; // Thay bằng namespace chứa DbContext của bạn nếu khác
using Web_Store.Models; // Namespace chứa các Model (Product, Category...)

namespace Web_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly Appdbcontext _context;

        // Inject AppDbContext vào Controller để thao tác với cơ sở dữ liệu
        public ProductController(Appdbcontext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action lọc danh sách sản phẩm theo mã danh mục (CategoryId)
        public async Task<IActionResult> Listpro(int id)
        {
            List<Product> listpro = await _context.Products
                                                  .Where(p => p.CategoryId == id)
                                                  .ToListAsync();
            return View(listpro);
        }
    }
}