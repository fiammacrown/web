using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Abeslamidze_Web.DAL.Data;
using Abeslamidze_Web.DAL.Entities;

namespace Abeslamidze_Web.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly Abeslamidze_Web.DAL.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public EditModel(Abeslamidze_Web.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dish = await _context.Dishes
                .Include(d => d.Group).FirstOrDefaultAsync(m => m.DishId == id);

            if (Dish == null)
            {
                return NotFound();
            }
           ViewData["DishGroupId"] = new SelectList(_context.DishGroups, "DishGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                if (Image != null)
                {
                    var fileName = $"{Dish.DishId}" +
                    Path.GetExtension(Image.FileName);
                    Dish.Image = fileName;
                    var path = Path.Combine(_environment.WebRootPath, "images/dishes",
                    fileName);
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(fStream);
                    }

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(Dish.DishId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }
    }
}
