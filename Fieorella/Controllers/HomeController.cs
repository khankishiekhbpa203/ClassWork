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
    }
}
