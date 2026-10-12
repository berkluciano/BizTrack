using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniProject1.Data;
using UniProject1.Models;

namespace UniProject1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TodoItemsController(AppDbContext db)
    {
        _db = db;
    }
    public class CommentDto
    {
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
    }

    // DTO за чист JSON без цикли
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        
        //TodoItem има nullable foreign key UserId към Users. Assignment-ът се прави чрез FK, а navigation property-то се използва за четене.
        public string? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string PriorityName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
    public class TodoItemCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }   // само Id, не обект
        public int StatusId { get; set; }     // само Id, не обект
        public int ProjectId { get; set; }    // само Id, не обект
        public bool IsCompleted { get; set; }
    }

    // GET: api/todoitems
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _db.TodoItems
    .Include(t => t.Priority)
    .Include(t => t.AssignedUser) 
    .Include(t => t.Status)
    .Include(t => t.Project)
    .Select(t => new TodoItemDto
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        AssignedTo = t.AssignedUser != null ? t.AssignedUser.Name : null,
        DueDate = t.DueDate,
        PriorityName = t.Priority.Name,
        StatusName = t.Status.Name,
        ProjectName = t.Project.Name,
        IsCompleted = t.IsCompleted,
        CreatedAt = t.CreatedAt
    })
    .ToListAsync();

        return Ok(items);
    }


    // POST: api/todoitems
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoItemCreateDto dto)
    {
        var priority = await _db.Priorities.FindAsync(dto.PriorityId);
        var status = await _db.Statuses.FindAsync(dto.StatusId);
        var project = await _db.Projects.FindAsync(dto.ProjectId);

        if (priority == null) return BadRequest($"Priority {dto.PriorityId} does not exist.");
        if (status == null) return BadRequest($"Status {dto.StatusId} does not exist.");
        if (project == null) return BadRequest($"Project {dto.ProjectId} does not exist.");
        

        var item = new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            AssignedTo = dto.AssignedTo,
            DueDate = dto.DueDate,
            PriorityId = dto.PriorityId,
            StatusId = dto.StatusId,
            ProjectId = dto.ProjectId,
            IsCompleted = dto.IsCompleted
        };


        _db.TodoItems.Add(item);
        await _db.SaveChangesAsync();

        // Връщаме GET DTO за по-чист JSON
        var resultDto = new TodoItemDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            AssignedTo = item.AssignedTo,
            DueDate = item.DueDate,
            PriorityName = priority.Name,
            StatusName = status.Name,
            ProjectName = project.Name,
            IsCompleted = item.IsCompleted,
            CreatedAt = item.CreatedAt
        };

        return CreatedAtAction(nameof(GetAll), new { id = item.Id }, resultDto);
    }

    // DELETE: api/todoitems/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item == null) return NotFound();

        _db.TodoItems.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // PATCH: api/todoitems/{id}/complete
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item == null) return NotFound();
        item.IsCompleted = true;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/uncomplete")]
    public async Task<IActionResult> Uncomplete(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item == null) return NotFound();
        item.IsCompleted = false;
        await _db.SaveChangesAsync();
        return NoContent();
    }
    [HttpGet("check-status")]
    public IActionResult CheckStatus()
    {
        return Ok(_db.Statuses.ToList());

    }
    [HttpGet("comments")]
    public IActionResult GetComments()
    {
        return Ok(_db.Comments.ToList());
    }

    [HttpGet("users/{userId}/tasks-projects")]
    public async Task<IActionResult> GetUserTasksAndProjects(int userId)
    {
        var user = await _db.Users
            .Include(u => u.TodoItems)                // задачи
                .ThenInclude(t => t.Project)         // проект на всяка задача
            .Include(u => u.ProjectUsers)            // M:N Project
                .ThenInclude(pu => pu.Project)       // проектите
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();

        var result = new
        {
            User = user.Name,
            Tasks = user.TodoItems.Select(t => new
            {
                t.Id,
                t.Title,
                ProjectName = t.Project.Name
            }),
            Projects = user.ProjectUsers.Select(pu => new
            {
                pu.Project.Id,
                pu.Project.Name
            })
        };

        return Ok(result);
    }
    [HttpGet("projects/{projectId}/users")]
    public async Task<IActionResult> GetUsersForProject(int projectId)
    {
        var project = await _db.Projects
            .Include(p => p.ProjectUsers)
                .ThenInclude(pu => pu.User)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) return NotFound();

        var result = new
        {
            Project = project.Name,
            Users = project.ProjectUsers.Select(pu => new
            {
                pu.User.Id,
                pu.User.Name
            })
        };

        return Ok(result);
    }

    [HttpGet("projects/{projectId}/tasks")]
    public async Task<IActionResult> GetTasksInProject(int projectId)
    {
        var tasks = await _db.TodoItemProjects
            .Where(tp => tp.ProjectId == projectId)
            .Include(tp => tp.TodoItem)
                .ThenInclude(t => t.Status)
            .Include(tp => tp.TodoItem)
                .ThenInclude(t => t.Priority)
            .Select(tp => new
            {
                tp.TodoItem.Id,
                tp.TodoItem.Title,
                Status = tp.TodoItem.Status.Name,
                Priority = tp.TodoItem.Priority.Name,
                tp.TodoItem.IsCompleted
            })
            .ToListAsync();

        return Ok(tasks);
    }
    [HttpGet("tasks/{taskId}/comments")]
    public async Task<IActionResult> GetCommentsForTask(int taskId)
    {
        var comments = await _db.Comments
            .Where(c => c.TodoItemId == taskId)
            .Include(c => c.User)
            .Select(c => new
            {
                c.Id,
                c.Content,
                UserName = c.User.Name,
                c.CreatedAt
            })
            .ToListAsync();

        return Ok(comments);
    }
    
    [HttpPost("tasks/{taskId}/comments")]
    public async Task<IActionResult> AddComment(
    int taskId,
    [FromBody] CommentDto dto)
    {
        // Проверка дали задачата съществува
        var todo = await _db.TodoItems.FindAsync(taskId);
        if (todo == null)
            return BadRequest($"TodoItem {taskId} not found.");

        // Проверка дали user съществува
        var user = await _db.Users.FindAsync(dto.UserId);
        if (user == null)
            return BadRequest($"User {dto.UserId} not found.");

        var comment = new Comment
        {
            Content = dto.Content,
            UserId = user.Id,
            TodoItemId = todo.Id,
            CreatedAt = DateTime.UtcNow
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            comment.Id,
            comment.Content,
            UserName = user.Name,
            TodoTitle = todo.Title,
            comment.CreatedAt
        });
    }
    [HttpPost("/api/projects/{projectId}/users/{userId}")]
    public async Task<IActionResult> AddUserToProject(int projectId, int userId)
    {
        if (!await _db.Projects.AnyAsync(p => p.Id == projectId))
            return NotFound("Project not found");

        if (!await _db.Users.AnyAsync(u => u.Id == userId))
            return NotFound("User not found");

        if (await _db.ProjectUsers.AnyAsync(pu =>
            pu.ProjectId == projectId && pu.UserId == userId))
            return BadRequest("User already in project");

        _db.ProjectUsers.Add(new ProjectUser
        {
            ProjectId = projectId,
            UserId = userId
        });

        await _db.SaveChangesAsync();
        return Ok("User added to project");
    }

    [HttpPost("/api/projects/{projectId}/tasks/{todoId}")]
    public async Task<IActionResult> AddTaskToProject(int projectId, int todoId)
    {
        if (!await _db.Projects.AnyAsync(p => p.Id == projectId))
            return NotFound("Project not found");

        if (!await _db.TodoItems.AnyAsync(t => t.Id == todoId))
            return NotFound("TodoItem not found");

        if (await _db.TodoItemProjects.AnyAsync(tp =>
            tp.ProjectId == projectId && tp.TodoItemId == todoId))
            return BadRequest("Task already in project");

        _db.TodoItemProjects.Add(new TodoItemProject
        {
            ProjectId = projectId,
            TodoItemId = todoId
        });

        await _db.SaveChangesAsync();
        return Ok("Task added to project");
    }
}
