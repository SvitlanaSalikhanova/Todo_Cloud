namespace TodoList.Models
{
    public class TodoListAddModel
    {
        public string Name { get; set; }
        public List<TodoItemAddModel> toDoItems { get; set; }
    }
}
