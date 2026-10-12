namespace UniProject1.Models
{
    public class TodoItemProject
    {
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; } = null!;
        public int ProjectId {  get; set; }
        public Project Project { get; set; } = null!;
         
    }
}
