namespace TaskBuddy.Tests
{

    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using TaskBuddy.Data;
    using TaskBuddy.Models;
    using Microsoft.AspNetCore.Http.HttpResults;

    public class TestTaskuri
    {
        [Fact]
        public void CanAddTask()
        {
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "CanAddTaskuri_Database")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var taskuri = new Taskuri {
                        Id = 1,
                        Name = "Test",
                        Type ="Test Type",
                        Description = "Test Description",
                        Priority = "Test Priority",
                        CreatedAt = new DateTime(2023, 12, 5, 13, 57, 0),
                        DeadLine = new DateTime(2023, 12, 12, 13, 57, 0),
                        IsCompleted = true
                    };

                    // Act
                    context.Taskuri.Add(taskuri);
                    context.SaveChanges();
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(1, context.Taskuri.Count());
                    Assert.Equal("Test", context.Taskuri.Single().Name);
                    Assert.Equal("Test Type", context.Taskuri.Single().Type);
                    Assert.Equal("Test Description", context.Taskuri.Single().Description);
                    Assert.Equal("Test Priority", context.Taskuri.Single().Priority);
                    Assert.Equal(new DateTime(2023, 12, 5, 13, 57, 0), context.Taskuri.Single().CreatedAt);
                    Assert.Equal(new DateTime(2023, 12, 12, 13, 57, 0), context.Taskuri.Single().DeadLine);
                    Assert.Equal(true, context.Taskuri.Single().IsCompleted);

                }
            }
        }
        [Fact]
        public void CanGetTaskuri()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CanGetTaskuri_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var taskuri = new Taskuri
                {
                    Id = 1,
                    Name = "Test",
                    Type = "Test Type",
                    Description = "Test Description",
                    Priority = "Test Priority",
                    CreatedAt = DateTime.UtcNow,
                    DeadLine = DateTime.UtcNow,
                    IsCompleted = true
                };
                context.Taskuri.Add(taskuri);
                context.SaveChanges();
            }

            // Act
            Taskuri retrievedTaskuri;
            using (var context = new ApplicationDbContext(options))
            {
                retrievedTaskuri = context.Taskuri.FirstOrDefault();
            }

            // Assert
            Assert.NotNull(retrievedTaskuri);
            Assert.Equal("Test Description", retrievedTaskuri.Description);
        }

        [Fact]
        public void CanUpdateTaskuri()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CanUpdateTaskuri_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var taskuri = new Taskuri
                {
                    Id = 1,
                    Name = "Test",
                    Type = "Test Type",
                    Description = "Original Task",
                    Priority = "Test Priority",
                    CreatedAt = DateTime.UtcNow,
                    DeadLine = DateTime.UtcNow,
                    IsCompleted = true
                };
                context.Taskuri.Add(taskuri);
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var taskuriToUpdate = context.Taskuri.Single();
                taskuriToUpdate.Description = "Updated Task";
                context.SaveChanges();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var updatedTaskuri = context.Taskuri.Single();
                Assert.Equal("Updated Task", updatedTaskuri.Description);
            }
        }

        [Fact]
        public void CanDeleteTaskuri()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CanDeleteTaskuri_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var taskuri = new Taskuri
                {
                    Id = 1,
                    Name = "Test",
                    Type = "Test Type",
                    Description = "Task to Delete",
                    Priority = "Test Priority",
                    CreatedAt = DateTime.UtcNow,
                    DeadLine = DateTime.UtcNow,
                    IsCompleted = true
                };
                context.Taskuri.Add(taskuri);
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var taskuriToDelete = context.Taskuri.Single();
                context.Taskuri.Remove(taskuriToDelete);
                context.SaveChanges();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Empty(context.Taskuri);
            }
        }
    }
}