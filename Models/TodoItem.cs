using System;
using System.ComponentModel.DataAnnotations;

namespace UniProject1.Models;

public class TodoItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? AssignedTo { get; set; }

    public DateTime? DueDate { get; set; }

    // Foreign Key към Priority
    [Required]
    public int PriorityId { get; set; }

    // Навигационно свойство към Priority
    public Priority? Priority { get; set; } // Може да е null при POST

    public int? UserId { get; set; }
    public User? AssignedUser { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int StatusId { get; set; }
    public Status Status { get; set; } 

    public int ProjectId { get; set; }
    public Project Project { get; set; } 

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<TodoItemProject> TodoItemProjects { get; set; } = new List<TodoItemProject>();

}
