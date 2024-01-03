


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Pages
{
    public class IndexCompletedModel : PageModel
    {
        private readonly TaskBuddy.Data.ApplicationDbContext _context;

        public IndexCompletedModel(TaskBuddy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Taskuri> Taskuri { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Criteriu { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Description { get; set; }

        public string DateSort { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            var taskuri = from task in _context.Taskuri
                          select task;
            switch (sortOrder)
            {

                case "Date":
                    taskuri = taskuri.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    taskuri = taskuri.OrderByDescending(s => s.CreatedAt);
                    break;

            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                taskuri = taskuri.Where(t => t.Priority.Contains(SearchString));
            }
            Taskuri = await taskuri.ToListAsync();
        }
    }
}
