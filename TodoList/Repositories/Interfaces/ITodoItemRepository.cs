using Microsoft.EntityFrameworkCore;
using TodoList.Entities;

namespace TodoList.Repositories.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<TodoItemEntity?> GetById(int id, CancellationToken cancellation);

        Task<bool> DeleteById(int id, CancellationToken cancellation);

        Task<TodoItemEntity> Add(TodoItemEntity item, CancellationToken cancellation);

        Task<TodoItemEntity?> Update(int id, TodoItemEntity newItem, CancellationToken cancellation);
    }
}
