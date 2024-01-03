using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TaskBuddy.Data.ApplicationDbContext _context;

        public DetailsModel(TaskBuddy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Taskuri Taskuri { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskuri = await _context.Taskuri.FirstOrDefaultAsync(m => m.Id == id);
            if (taskuri == null)
            {
                return NotFound();
            }
            else
            {
                Taskuri = taskuri;
            }
            return Page();
        }
    }
}
