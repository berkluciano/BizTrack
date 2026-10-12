namespace UniProject1.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<TodoItemProject> TodoItemProjects { get; set; } = new List<TodoItemProject>();

    }
}
