using Fieorella.DAL;
using Fieorella.Models;
using Fieorella.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fieorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            List<Product> products = _context.Products.ToList();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Products = products
            };
            return View(homeVM);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Product? product = _context.Products.Include(c => c.Categories).FirstOrDefault(p => p.Id == id);


            if (product == null) return NotFound();

            //var relatedproducts = _context.Products
            //    .Where( && p.Id!=id)
            //    .Take(3)
            //    .ToList();
            DetailVM detailVM = new DetailVM
            {
                Product = product,
                //RelatedProducts = relatedproducts,
            };
            return View(detailVM);
        }
    }
}
