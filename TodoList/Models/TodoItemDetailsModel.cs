using TodoList.Entities;

namespace TodoList.Models
{
    public class TodoItemDetailsModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int? TodoListId { get; set; }
        public bool IsDone { get; set; }
    }
}
