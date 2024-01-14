using System;
using System.Collections.Generic;
using Bogus;
using TaskBuddy.Models;
namespace TaskBuddy.Class
{
    public class AddMock
    {
        public List<Taskuri> GenerateTasks(int numberOfTasks)
        {
            var faker = new Faker<Taskuri>()
                .RuleFor(t => t.Name, f => f.Lorem.Word())
                .RuleFor(t => t.Type, f => f.Random.Enum<TaskType>().ToString())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Priority, f => f.PickRandom("Prio 0", "Prio 1", "Prio 2"))
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.DeadLine, f => f.Date.Future())
                .RuleFor(t => t.IsCompleted, f => f.Random.Bool());

            var tasks = faker.Generate(numberOfTasks);
            return tasks;
        }
        public enum TaskType 
        { 
            Bug,
            Feature,
            Refactor   
        }
    }
}
