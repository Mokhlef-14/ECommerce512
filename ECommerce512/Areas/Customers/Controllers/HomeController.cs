using System.Diagnostics;
using System.Linq;
using ECommerce512.Data;
using ECommerce512.Models;
using ECommerce512.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce512.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index(int categortId,string? query,double MinPrice,double Maxprice,int page = 1)    
        {
            IQueryable<Product> products = _context.products.Include(e => e.Category);
            var categories = _context.categories;
            ViewData["categories"] = categories.ToList();

            if (categortId > 0 && categortId < categories.Count())
            {
                products = products.Where(e => e.CategoryId == categortId);
                ViewBag.categoryId = categortId;
            }

            if(query is not null)
            {
                products = products.Where(e => e.Name.Contains(query));
                ViewBag.query = query;
            }

            if (MinPrice > 0)
            {
                products = products.Where(e => e.Price >= (decimal)MinPrice);
                ViewBag.MinPrice = MinPrice;
            }
            if (Maxprice > 0)
            {
                products = products.Where(e => e.Price <= (decimal)Maxprice);
                ViewBag.MaxPrice = Maxprice;
            }

            products = products.Skip((page-1) * 8).Take(8);
            ViewBag.TotalProducts = Math.Ceiling(_context.products.Count() / 8.0);
           
            
            
            

            return View(products.ToList());
            
        }


       
        public IActionResult Details(int id)
        {
            var product = _context.products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

          

            if (product != null)
            {
                var RelatedProducts = _context.products.Where(e => e.Name.Contains(product.Name.Substring(0,5))).Skip(0).Take(4).ToList();
                var sameCategoryProducts = _context.products.Where(e => e.Category.Name == product.Category.Name && e.Id != product.Id).OrderBy(e => Guid.NewGuid())
    .Take(4)
    .ToList();


                var ReturnedData = new ProductWithRelatedVM()
                {
                    Product = product,
                    RelatedProducts = RelatedProducts,
                    SameCategoryProducts = sameCategoryProducts
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
