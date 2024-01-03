using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskBuddy.Models
{

    public class Taskuri

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsCompleted { get; set; }


        public bool IsDeadlineNear(int daysThreshold = 3)
        {

            TimeSpan timeUntilDeadline = DeadLine - DateTime.Now;
            int daysUntilDeadline = (int)timeUntilDeadline.TotalDays;

            return daysUntilDeadline <= daysThreshold && daysUntilDeadline >= 0;
        }


        public static IEnumerable<SelectListItem>? TypeOptions()
        {
            return new[]
            {
                new SelectListItem { Text = "Bug", Value = "Bug"},
                new SelectListItem { Text = "Feature", Value = "Feature"},
                new SelectListItem { Text = "Refactor", Value = "Refactor"}
              };
        }

        public static IEnumerable<SelectListItem>? PryoOptions()
        {
            return new[]
            {
                new SelectListItem { Text = "Prio 0", Value = "Prio 0"},
                new SelectListItem { Text = "Prio 1", Value = "Prio 1"},
                new SelectListItem { Text = "Prio 2", Value = "Prio 2"}
              };
        }
    }
}
