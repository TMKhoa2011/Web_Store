using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Store.AppData;
using Web_Store.Models;

namespace Web_Store.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly Appdbcontext _context;

        public CartController(Appdbcontext context)
        {
            _context = context;
        }

        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        // Trang giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Thêm sản phẩm vào giỏ (nhận id và quantity nếu có)
        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            if (quantity < 1) quantity = 1;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductId == id);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name ?? $"Product {product.Id}",
                    Image = product.Image ?? "",
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng khi bấm nút "Update"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int[] productIds, int[] quantities)
        {
            var cart = GetCart();

            if (productIds != null && quantities != null && productIds.Length == quantities.Length)
            {
                for (int i = 0; i < productIds.Length; i++)
                {
                    var item = cart.FirstOrDefault(c => c.ProductId == productIds[i]);
                    if (item != null)
                    {
                        if (quantities[i] <= 0)
                            cart.Remove(item);
                        else
                            item.Quantity = quantities[i];
                    }
                }
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // Xóa 1 sản phẩm khỏi giỏ
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
    }
}
