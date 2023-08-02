namespace TodoList.Entities
{
    public class TodoItemEntity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public int? TodoListId { get; set; } 
        public TodoListEntity? TodoList { get; set; }
    }
}
