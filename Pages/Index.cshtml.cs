using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Class;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TaskBuddy.Data.ApplicationDbContext _context;

        public IndexModel(TaskBuddy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Taskuri> Taskuri { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Criteriu { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Description  {get; set; }

        [BindProperty]
        public List<string> SelectedPrio { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // SelectedPrio will contain the values of checked checkboxes
            if (SelectedPrio != null && SelectedPrio.Count > 0)
            {
                // Filter the list based on selected priorities
                var taskuri = from task in _context.Taskuri
                              select task;


                taskuri = taskuri.Where(item => SelectedPrio.Contains(item.Priority));
                Taskuri = await taskuri.ToListAsync();

            }
            AddMock mocker = new AddMock();
            var tasks = mocker.GenerateTasks(10);
            foreach (var task in tasks)
            {
                _context.Taskuri.Add(task);
            }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
        }

        public string DateSort { get; set; } = "desc";
        public string TagSort { get; set; } = "asc";


        public async Task OnGetAsync(string dateSort, string typeSort)
        {
            var taskuri = from task in _context.Taskuri
                          select task;

            if (!string.IsNullOrEmpty(dateSort))
            {
                DateSort = dateSort == "asc" ? "desc" : "asc";
                switch (dateSort)
                {

                    case "asc":
                        taskuri = taskuri.OrderBy(s => s.CreatedAt);
                        break;
                    case "desc":
                        taskuri = taskuri.OrderByDescending(s => s.CreatedAt);
                        break;

                }
            }

            if (!string.IsNullOrEmpty(typeSort))
            {
                TagSort = typeSort == "asc" ? "desc" : "asc";
                switch (typeSort)
                {

                    case "asc":
                        taskuri = taskuri.OrderBy(s => s.Type);
                        break;
                    case "desc":
                        taskuri = taskuri.OrderByDescending(s => s.Type);
                        break;

                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                taskuri = taskuri.Where(t => t.Name.Contains(SearchString));
            }
            Taskuri = await taskuri.ToListAsync();


            List<Taskuri> TaskNotification = taskuri.ToList();
            foreach (Taskuri myTask in TaskNotification)
            if (myTask.IsDeadlineNear())
            {
                Console.WriteLine("The task is nearing its deadline!");
           
                }
            else
            {
                Console.WriteLine("The task is not nearing its deadline yet.");
            }

        }
    }
}
