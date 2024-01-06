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
    public class StatsModel : PageModel
    {
        private readonly TaskBuddy.Data.ApplicationDbContext _context;

        public StatsModel(TaskBuddy.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Taskuri> Taskuri { get; set; } = default!;
        public float allTaskCount { get; set; }
        public float completedTaskCount { get; set; }
        public float incompletedTaskCount { get; set; }
        public float procentajCompleted { get; set; }
        public float procentajIncompleted { get; set; }

        public async Task OnGetAsync()
        {
            var taskuri = from task in _context.Taskuri
                              select task;
            Taskuri = await taskuri.ToListAsync();
            allTaskCount = Taskuri.Count;

            completedTaskCount = await _context.Taskuri
                                          .Where(task => task.IsCompleted == true)
                                          .CountAsync();

            incompletedTaskCount = allTaskCount - completedTaskCount;
            procentajCompleted = (float)Math.Round(completedTaskCount * 100 / allTaskCount,2, MidpointRounding.AwayFromZero) ;
            procentajIncompleted = 100 - procentajCompleted;

            

        }
    }
}
