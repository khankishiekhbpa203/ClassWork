using Fieorella.DAL;
using Fieorella.Models;
using Fieorella.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fieorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Category>categories= await _context.Categories.ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name==null)
            {
                return View();
            } 
        
            bool existcategory = await _context.Categories.AnyAsync(c=>c.Name.Trim()==category.Name.Trim());

            if (existcategory)
            {
                ModelState.AddModelError(nameof(category.Name), "name cant be same");
                return View();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(category);
            //}
     
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int?id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int?id,Category category)
        {
            if (id == null || id < 1) return BadRequest();

            Category existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (existCategory == null) return NotFound();

            existCategory.Name = category.Name;

            _context.Update(existCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
