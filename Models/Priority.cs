using System.Collections.Generic;

namespace UniProject1.Models;

public class Priority
{
    public int Id { get; set; }  // 1, 2, 3
    public string Name { get; set; } = null!;

    // Навигационно свойство: 1:N към TodoItem
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
