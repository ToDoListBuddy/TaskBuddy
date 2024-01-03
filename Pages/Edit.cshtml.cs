using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Pages
{
    public class EditModel : PageModel
    {
        private readonly TaskBuddy.Data.ApplicationDbContext _context;

        public EditModel(TaskBuddy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Taskuri Taskuri { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskuri =  await _context.Taskuri.FirstOrDefaultAsync(m => m.Id == id);
            if (taskuri == null)
            {
                return NotFound();
            }
            Taskuri = taskuri;
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

            _context.Attach(Taskuri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskuriExists(Taskuri.Id))
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

        private bool TaskuriExists(int id)
        {
            return _context.Taskuri.Any(e => e.Id == id);
        }
    }
}
