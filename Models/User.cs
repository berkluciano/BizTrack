using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UniProject1.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CommentUser> CommentUsers { get; set; } = new List<CommentUser>();

}
