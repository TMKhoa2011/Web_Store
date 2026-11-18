using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Store.AppData;
using Web_Store.Data;
using Web_Store.Models;

namespace Web_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly Appdbcontext _context;

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

        // === BỔ SUNG THÊM ACTION NÀY ĐỂ XEM CHI TIẾT SẢN PHẨM ===
        public async Task<IActionResult> Detail(int id)
        {
            // Tìm sản phẩm trong cơ sở dữ liệu theo Id
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Trả về đúng tên file view "productdetail" kèm theo dữ liệu sản phẩm
            return View("productdetail", product);
        }
    }
}