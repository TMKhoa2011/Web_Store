using Microsoft.AspNetCore.Mvc;
using Web_Store.AppData;
using Web_Store.Models;

namespace Web_Store.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Appdbcontext _context;

        public RegisterController(Appdbcontext context)
        {
            _context = context;
        }

        // 1. Hiển thị trang Đăng ký (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 2. Xử lý logic khi người dùng bấm nút Submit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem Email hoặc Username đã tồn tại trong CSDL chưa (tùy thuộc vào bảng User của bạn)
                // Ví dụ nếu bạn có bảng Users trong DbContext:
                /*
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email này đã được sử dụng.");
                    return View(model);
                }

                // Tạo đối tượng User mới để lưu vào database
                var user = new User {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password // Lưu ý thực tế nên mã hóa mật khẩu (hash)
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                */

                // Tạm thời nếu đăng ký thành công, chuyển hướng về trang chủ hoặc trang đăng nhập
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Index", "Home");
            }

            // Nếu dữ liệu nhập không hợp lệ, hiển thị lại form kèm lỗi
            return View(model);
        }
    }
}
