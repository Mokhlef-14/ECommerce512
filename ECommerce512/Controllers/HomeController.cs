using System.Diagnostics;
using ECommerce512.Data;
using ECommerce512.Models;
using ECommerce512.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce512.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()    
        {
            var products = _context.products.Include(e => e.Category);
            return View(products.ToList());
        }


       
        public IActionResult Details(int id)
        {
            var product = _context.products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

          

            if (product != null)
            {
                var RelatedProducts = _context.products.Where(e => e.Name.Contains(product.Name.Substring(0,5))).Skip(0).Take(4).ToList();

                var ReturnedData = new ProductWithRelatedVM()
                {
                    Product = product,
                    RelatedProducts = RelatedProducts
                };
                return View(ReturnedData);
            }

            return RedirectToAction(nameof(NotFoundPage));

        }

        public IActionResult NotFoundPage()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
