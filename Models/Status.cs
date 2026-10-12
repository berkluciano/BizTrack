namespace UniProject1.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // New, In Progress, Done

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
