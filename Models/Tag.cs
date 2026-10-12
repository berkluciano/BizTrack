namespace UniProject1.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public ICollection<TodoItemTag> TodoItemTags { get; set; } = new List<TodoItemTag>();
    }

}
