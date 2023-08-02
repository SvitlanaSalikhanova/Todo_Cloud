using TodoList.Entities;

namespace TodoList.Models
{
    public class TodoListDetailsModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<TodoItemDetailsModel> TodoItems { get; set; } = new();
    }
}
