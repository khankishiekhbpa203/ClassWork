using Fieorella.DAL;
using Fieorella.Models;
using Fieorella.Utilities.Enums;
using Fieorella.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fieorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
       
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
               if (slider.Photo is null)
               {
                ModelState.AddModelError(nameof(slider.Photo), "Image cant be empty");
                return View();
               }
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type is incorrect");
                    return View();
                }

                if (!slider.Photo.CheckFileSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError("Photo", "File size must be less than 2mb");
                    return View();
                } 
                slider.ImageURL = await slider.Photo.CreateFileAsync(_env.WebRootPath, "img");

                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int ?id)
        {
            if (id == null||id<1) return BadRequest();

            Slider slider= await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);

            if (slider == null) return NotFound();

            slider.ImageURL.DeleteFile(_env.WebRootPath, "img");
            _context.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int ?id,Slider slider)
        {
            if (id == null || id < 1) return BadRequest();

            if (slider.Photo != null)
            {
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type is incorrect");
                    return View();
                }

                if (!slider.Photo.CheckFileSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError("Photo", "File size must be less than 2mb");
                    return View();
                } 
                Slider existslider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

                if (existslider == null) return NotFound();
                existslider.ImageURL =await slider.Photo.CreateFileAsync(_env.WebRootPath, "img");
            }

          

           
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
