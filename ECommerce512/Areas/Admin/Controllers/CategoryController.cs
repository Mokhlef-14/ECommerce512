using ECommerce512.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce512.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var Categories =  _context.categories;
            return View(Categories.ToList());
        }
    }
}
