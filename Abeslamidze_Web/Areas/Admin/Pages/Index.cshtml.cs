using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Abeslamidze_Web.DAL.Data;
using Abeslamidze_Web.DAL.Entities;

namespace Abeslamidze_Web.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Abeslamidze_Web.DAL.Data.ApplicationDbContext _context;

        public IndexModel(Abeslamidze_Web.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Dish> Dish { get;set; }

        public async Task OnGetAsync()
        {
            Dish = await _context.Dishes
                .Include(d => d.Group).ToListAsync();
        }
    }
}
