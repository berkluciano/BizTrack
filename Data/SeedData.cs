//using UniProject1.Models;
//using UniProject1.Data;
//using Microsoft.EntityFrameworkCore;

//namespace UniProject1.Data;

//public static class SeedData
//{
//    public static void Initialize(AppDbContext context)
//    {
//        context.Database.Migrate();

//        // --- Priorities ---
//        if (!context.Priorities.Any())
//        {
//            context.Priorities.AddRange(
//                new Priority { Id = 1, Name = "Low" },
//                new Priority { Id = 2, Name = "Medium" },
//                new Priority { Id = 3, Name = "High" }
//            );
//        }

//        // --- Users ---
//        if (!context.Users.Any())
//        {
//            context.Users.AddRange(
//                new User { Name = "Ivan" },
//                new User { Name = "Maria" },
//                new User { Name = "Peter" },
//                new User { Name = "Anna" }
//            );
//        }

//        context.SaveChanges(); // запазваме първоначалните данни

//        // --- Roles ---
//        if (!context.Roles.Any())
//        {
//            context.Roles.AddRange(
//                new Role { Name = "Admin" },
//                new Role { Name = "Manager" },
//                new Role { Name = "Employee" }
//            );
//        }

//        context.SaveChanges();

//        // --- UserRoles (M:N) ---
//        if (!context.UserRoles.Any())
//        {
//            var users = context.Users.ToList();
//            var roles = context.Roles.ToList();

//            context.UserRoles.AddRange(
//                new UserRole { UserId = users[0].Id, RoleId = roles[0].Id },
//                new UserRole { UserId = users[1].Id, RoleId = roles[1].Id },
//                new UserRole { UserId = users[2].Id, RoleId = roles[2].Id },
//                new UserRole { UserId = users[3].Id, RoleId = roles[2].Id }
//            );
//        }

//        context.SaveChanges();

//        // --- Projects ---
//        if (!context.Projects.Any())
//        {
//            context.Projects.AddRange(
//                new Project { Name = "Project Alpha", Description = "Тестов проект 1" },
//                new Project { Name = "Project Beta", Description = "Тестов проект 2" }
//            );
//        }

//        context.SaveChanges();

//        // --- ProjectUsers (M:N) ---
//        if (!context.ProjectUsers.Any())
//        {
//            var projects = context.Projects.ToList();
//            var users = context.Users.ToList();

//            context.ProjectUsers.AddRange(
//                new ProjectUser { ProjectId = projects[0].Id, UserId = users[0].Id },
//                new ProjectUser { ProjectId = projects[0].Id, UserId = users[1].Id },
//                new ProjectUser { ProjectId = projects[1].Id, UserId = users[2].Id },
//                new ProjectUser { ProjectId = projects[1].Id, UserId = users[3].Id }
//            );
//        }

//        context.SaveChanges();

//        // --- Statuses ---
//        if (!context.Statuses.Any())
//        {
//            context.Statuses.AddRange(
//                new Status { Id = 1, Name = "New" },
//                new Status { Id = 2, Name = "In Progress" },
//                new Status { Id = 3, Name = "Done" }
//            );
//        }

//        context.SaveChanges();

//        // --- Comments ---
//        if (!context.Comments.Any())
//        {
//            var users = context.Users.ToList();
//            context.Comments.AddRange(
//                new Comment { Text = "Тестов коментар 1", UserId = users[0].Id, CreatedAt = DateTime.UtcNow },
//                new Comment { Text = "Тестов коментар 2", UserId = users[1].Id, CreatedAt = DateTime.UtcNow }
//            );
//        }

//        context.SaveChanges();

//        // --- TodoItems ---
//        if (!context.TodoItems.Any())
//        {
//            var users = context.Users.ToList();
//            var priorities = context.Priorities.ToList();
//            var statuses = context.Statuses.ToList();

//            context.TodoItems.AddRange(
//                new TodoItem
//                {
//                    Title = "Първа задача",
//                    Description = "Тест seed",
//                    UserId = users[0].Id,
//                    PriorityId = priorities[1].Id, // Medium
//                    StatusId = statuses[0].Id,     // New
//                    DueDate = DateTime.UtcNow.AddDays(3),
//                    IsCompleted = false
//                },
//                new TodoItem
//                {
//                    Title = "Втора задача",
//                    Description = "Втори тест",
//                    UserId = users[1].Id,
//                    PriorityId = priorities[2].Id, // High
//                    StatusId = statuses[1].Id,     // In Progress
//                    DueDate = DateTime.UtcNow.AddDays(5),
//                    IsCompleted = false
//                }
//            );
//        }
//        if (!context.TodoItemProjects.Any())
//        {
//            var todo = context.TodoItems.First();
//            var project = context.Projects.First();

//            context.TodoItemProjects.Add(new TodoItemProject
//                { 
//            TodoItemId=todo.Id,
//                    ProjectId = project.Id
//            });

//        }
//        if (!context.Comments.Any())
//        {
//            var comment = context.Comments.First();
//            var user = context.Users.First();

//            context.CommentUsers.Add(new CommentUser
//            {
//                CommentId = comment.Id,
//                UserId = user.Id
//            });
//        }

//        context.SaveChanges();
//    }
//}
using UniProject1.Data;
using UniProject1.Models;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.Migrate();

        // ===== BASE TABLES =====
        if (!context.Priorities.Any())
        {
            context.Priorities.AddRange(
                new Priority { Name = "Low" },
                new Priority { Name = "Medium" },
                new Priority { Name = "High" }
            );
        }

        if (!context.Statuses.Any())
        {
            context.Statuses.AddRange(
                new Status { Name = "New" },
                new Status { Name = "In Progress" },
                new Status { Name = "Done" }
            );
        }

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Name = "Ivan" },
                new User { Name = "Maria" },
                new User { Name = "Peter" },
                new User { Name = "Anna" }
            );
        }

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Name = "Admin" },
                new Role { Name = "Manager" },
                new Role { Name = "Employee" }
            );
        }

        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new Project { Name = "Project Alpha", Description = "Основен проект" },
                new Project { Name = "Project Beta", Description = "Втори проект" }
            );
        }

        context.SaveChanges();

        // ===== TODO ITEMS =====
        if (!context.TodoItems.Any())
        {
            var users = context.Users.ToList();
            var priorities = context.Priorities.ToList();
            var statuses = context.Statuses.ToList();
            var projects = context.Projects.ToList();

            context.TodoItems.AddRange(
                new TodoItem
                {
                    Title = "Първа задача",
                    Description = "Seed задача 1",
                    UserId = users[0].Id,
                    PriorityId = priorities[1].Id,
                    StatusId = statuses[0].Id,
                    ProjectId = projects[0].Id,
                    DueDate = DateTime.UtcNow.AddDays(3)
                },
                new TodoItem
                {
                    Title = "Втора задача",
                    Description = "Seed задача 2",
                    UserId = users[1].Id,
                    PriorityId = priorities[2].Id,
                    StatusId = statuses[1].Id,
                    ProjectId = projects[1].Id,
                    DueDate = DateTime.UtcNow.AddDays(5)
                }
            );
        }

        context.SaveChanges();

        // ===== COMMENTS =====
        if (!context.Comments.Any())
        {
            var todo = context.TodoItems.First();
            var users = context.Users.ToList();

            context.Comments.AddRange(
                new Comment { Content = "Първи коментар", UserId = users[0].Id, TodoItemId = todo.Id },
                new Comment { Content = "Втори коментар", UserId = users[1].Id, TodoItemId = todo.Id }
            );
        }

        context.SaveChanges();

        // ===== M:N TABLES =====
        if (!context.ProjectUsers.Any())
        {
            var projects = context.Projects.ToList();
            var users = context.Users.ToList();

            context.ProjectUsers.AddRange(
                new ProjectUser { ProjectId = projects[0].Id, UserId = users[0].Id },
                new ProjectUser { ProjectId = projects[0].Id, UserId = users[1].Id },
                new ProjectUser { ProjectId = projects[1].Id, UserId = users[2].Id }
            );
        }

        if (!context.UserRoles.Any())
        {
            var users = context.Users.ToList();
            var roles = context.Roles.ToList();

            context.UserRoles.AddRange(
                new UserRole { UserId = users[0].Id, RoleId = roles[0].Id },
                new UserRole { UserId = users[1].Id, RoleId = roles[1].Id }
            );
        }

        if (!context.TodoItemProjects.Any())
        {
            var todo = context.TodoItems.First();
            var project = context.Projects.First();

            context.TodoItemProjects.Add(
                new TodoItemProject { TodoItemId = todo.Id, ProjectId = project.Id }
            );
        }

        context.SaveChanges();
    }
}
