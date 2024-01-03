using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Data;
using TaskBuddy.Models;
using Xunit;

namespace TaskBuddy.Tests
{
    public class Notificari
    {

        [Fact]
        public void VerifyDeadLine()
        {
            {
                // Arrange

                var taskuri = new List<Taskuri>
{
                        new Taskuri
                        {
                            Id = 1,
                            Name = "Test",
                            Type = "Test Type",
                            Description = "Test Description",
                            Priority = "Test Priority",
                            CreatedAt = new DateTime(2023, 12, 11, 13, 57, 0),
                            DeadLine = new DateTime(2023, 12, 12, 13, 57, 0),
                            IsCompleted = true
                        },
                        new Taskuri
                        {
                            Id = 2,
                            Name = "Test",
                            Type = "Test Type",
                            Description = "Test Description",
                            Priority = "Test Priority",
                            CreatedAt = new DateTime(2023, 12, 14, 13, 57, 0),
                            DeadLine = new DateTime(2023, 12, 15, 13, 57, 0),
                            IsCompleted = true
                        },
                               new Taskuri
                        {
                            Id = 3,
                            Name = "Test",
                            Type = "Test Type",
                            Description = "Test Description",
                            Priority = "Test Priority",
                            CreatedAt = new DateTime(2023, 12, 14, 13, 57, 0),
                            DeadLine = new DateTime(2023, 12, 16, 13, 57, 0),
                            IsCompleted = true
                        },
                                      new Taskuri
                        {
                            Id = 4,
                            Name = "Test",
                            Type = "Test Type",
                            Description = "Test Description",
                            Priority = "Test Priority",
                            CreatedAt = new DateTime(2023, 12, 14, 13, 57, 0),
                            DeadLine = new DateTime(2023, 12, 20, 13, 57, 0),
                            IsCompleted = true
                        }
                    };


                // Assert

                Assert.Equal(2, taskuri.Count(task => task.IsDeadlineNear()));

            }
        }
    }
}
