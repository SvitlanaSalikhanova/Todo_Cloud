namespace TodoList.Entities
{
    public class TodoListEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<TodoItemEntity> TodoItems { get; set; } = new();

    }
}
