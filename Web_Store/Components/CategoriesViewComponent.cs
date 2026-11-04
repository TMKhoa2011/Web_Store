using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Store.AppData;
using Web_Store.Data; // Đảm bảo namespace DbContext của bạn
using Web_Store.Models; // Namespace chứa model Category

namespace Web_Store.ViewComponents
{
    // Đổi từ CategoriesViewComponents thành CategoriesViewComponent (bỏ chữ s)
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly Appdbcontext _context;

        public CategoriesViewComponent(Appdbcontext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> cateList = await this._context.Categories.ToListAsync();
            return View(cateList);
        }
    }
}